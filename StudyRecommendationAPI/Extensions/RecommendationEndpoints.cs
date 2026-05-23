using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;
using StudyRecommendationAPI.Services;

namespace StudyRecommendationAPI.Extensions;

public static class RecommendationEndpoints
{
    public static void MapRecommendationEndpoints(this WebApplication app)
    {
        app.MapGet("/api/recommendations/{topicId:int}", async (int topicId, AppDbContext db, YouTubeService youtube, ClaudeService claude) =>
        {
            Topic? topic = await db.Topics
                .Include(t => t.Subject)
                .Include(t => t.Resources)
                .FirstOrDefaultAsync(t => t.Id == topicId);

            if (topic == null)
                return Results.Problem($"No se encontró el tema con ID {topicId}.", statusCode: 404);

            if (topic.Resources.Count == 0)
            {
                List<ResourceSuggestion> suggestions = [];

                if (youtube.IsEnabled)
                {
                    // Real videos from YouTube + Claude for articles
                    List<ResourceSuggestion> videos = await youtube.SearchVideosAsync(
                        topic.TopicName, topic.Subject.Name);
                    List<ResourceSuggestion> claudeSuggestions = await claude.GetResourceRecommendationsAsync(
                        topic.TopicName, topic.Subject.Name);

                    suggestions.AddRange(videos);
                    suggestions.AddRange(claudeSuggestions.Where(s => s.Type != "video"));
                }
                else
                {
                    // No YouTube key: Claude handles everything
                    suggestions = await claude.GetResourceRecommendationsAsync(
                        topic.TopicName, topic.Subject.Name);
                }

                if (suggestions.Count > 0)
                {
                    List<Resource> newResources = suggestions.Select(s => new Resource
                    {
                        TopicId = topic.Id,
                        Type = s.Type,
                        Title = s.Title,
                        Url = s.Url,
                        Source = s.Source,
                        PositiveVotes = 0,
                        NegativeVotes = 0,
                        CreatedAt = DateTime.UtcNow
                    }).ToList();

                    db.Resources.AddRange(newResources);
                    await db.SaveChangesAsync();
                    topic.Resources = newResources;
                }
            }

            List<ResourceDto> resources = topic.Resources
                .Select(r =>
                {
                    int total = r.PositiveVotes + r.NegativeVotes;
                    double rating = total == 0 ? 0 : Math.Round((double)r.PositiveVotes / total, 2);
                    return new ResourceDto
                    {
                        ResourceId = r.Id,
                        Type = r.Type,
                        Title = r.Title,
                        Url = r.Url,
                        Source = r.Source,
                        Rating = rating,
                        TotalVotes = total
                    };
                })
                .OrderByDescending(r => r.TotalVotes > 0 ? 1 : 0)
                .ThenByDescending(r => r.Rating)
                .ThenByDescending(r => r.TotalVotes)
                .ToList();

            RecommendationsResponse response = new()
            {
                TopicId = topic.Id,
                TopicName = topic.TopicName,
                SubjectName = topic.Subject.Name,
                Resources = resources
            };

            return Results.Ok(response);
        })
        .WithName("GetRecommendations")
        .WithTags("Recomendaciones")
        .WithSummary("Devuelve recursos educativos de un tema ordenados por rating")
        .Produces<RecommendationsResponse>()
        .ProducesProblem(404);
    }
}
