using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;
using StudyRecommendationAPI.Services;

namespace StudyRecommendationAPI.Extensions;

public static class SearchEndpoints
{
    public static void MapSearchEndpoints(this WebApplication app)
    {
        app.MapPost("/api/search", async (SearchRequest request, AppDbContext db, ClaudeCodeService claudeCode) =>
        {
            if (string.IsNullOrWhiteSpace(request.Topic))
                return Results.Problem("El tema es requerido.", statusCode: 400);

            string topic = request.Topic.Trim();

            // Return cached result if topic was already searched
            SearchResult? cached = await db.SearchResults
                .FirstOrDefaultAsync(s => s.Topic.ToLower() == topic.ToLower());

            if (cached != null)
                return Results.Ok(ToResponse(cached, fromCache: true));

            // Call Claude Code to search
            string prompt = BuildSearchPrompt(topic);
            (bool success, string result, string error) = await claudeCode.RunPromptAsync(prompt);

            if (!success)
                return Results.Ok(new SearchResponse { Topic = topic, Success = false, Error = error });

            (string? videoUrl, string? articleUrl) = ParseUrls(result);

            SearchResult searchResult = new()
            {
                Topic = topic,
                VideoUrl = videoUrl,
                ArticleUrl = articleUrl,
                CreatedAt = DateTime.UtcNow
            };

            db.SearchResults.Add(searchResult);
            await db.SaveChangesAsync();

            return Results.Ok(ToResponse(searchResult, fromCache: false));
        })
        .WithName("SearchResources")
        .WithTags("Búsqueda")
        .WithSummary("Busca 1 video y 1 artículo para un tema. Usa caché de DB en búsquedas repetidas.")
        .Produces<SearchResponse>()
        .ProducesProblem(400);

        app.MapPost("/api/search/{id:int}/rate", async (int id, RateSearchRequest request, AppDbContext db) =>
        {
            if (request.ResourceType != "video" && request.ResourceType != "article")
                return Results.Problem("ResourceType debe ser 'video' o 'article'.", statusCode: 400);

            SearchResult? searchResult = await db.SearchResults.FindAsync(id);
            if (searchResult == null)
                return Results.Problem($"No se encontró el resultado con ID {id}.", statusCode: 404);

            if (request.ResourceType == "video")
            {
                if (request.IsPositive) searchResult.VideoPositiveVotes++;
                else searchResult.VideoNegativeVotes++;
            }
            else
            {
                if (request.IsPositive) searchResult.ArticlePositiveVotes++;
                else searchResult.ArticleNegativeVotes++;
            }

            await db.SaveChangesAsync();
            return Results.Ok(ToResponse(searchResult, fromCache: true));
        })
        .WithName("RateSearchResult")
        .WithTags("Búsqueda")
        .WithSummary("Califica el video o artículo de un resultado de búsqueda")
        .Produces<SearchResponse>()
        .ProducesProblem(400)
        .ProducesProblem(404);
    }

    private static SearchResponse ToResponse(SearchResult s, bool fromCache) => new()
    {
        Id = s.Id,
        Topic = s.Topic,
        Success = true,
        FromCache = fromCache,
        Video = s.VideoUrl,
        VideoPositiveVotes = s.VideoPositiveVotes,
        VideoNegativeVotes = s.VideoNegativeVotes,
        Article = s.ArticleUrl,
        ArticlePositiveVotes = s.ArticlePositiveVotes,
        ArticleNegativeVotes = s.ArticleNegativeVotes
    };

    private static string BuildSearchPrompt(string topic) =>
        $"Usá WebSearch dos veces: primero buscá \"site:youtube.com {topic} tutorial\" y después buscá \"{topic} guía artículo\". Con los resultados respondé ÚNICAMENTE con estas dos líneas sin ningún texto extra: VIDEO: <url de youtube> ARTICLE: <url del artículo>";

    private static (string? video, string? article) ParseUrls(string result)
    {
        string? video = null;
        string? article = null;

        foreach (string line in result.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (line.StartsWith("VIDEO:", StringComparison.OrdinalIgnoreCase))
                video = line["VIDEO:".Length..].Trim();
            else if (line.StartsWith("ARTICLE:", StringComparison.OrdinalIgnoreCase))
                article = line["ARTICLE:".Length..].Trim();
        }

        // Fallback: extract URLs from markdown
        if (video == null || article == null)
        {
            System.Text.RegularExpressions.MatchCollection matches =
                System.Text.RegularExpressions.Regex.Matches(result, @"https?://[^\s\)\]""]+");

            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string url = match.Value.TrimEnd('.', ',', ')');
                if (video == null && (url.Contains("youtube.com/watch") || url.Contains("youtu.be")))
                    video = url;
                else if (article == null && !url.Contains("youtube.com") && !url.Contains("youtu.be"))
                    article = url;

                if (video != null && article != null) break;
            }
        }

        return (video, article);
    }
}
