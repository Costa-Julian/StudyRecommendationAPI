using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;

namespace StudyRecommendationAPI.Extensions;

public static class RecommendationEndpoints
{
    public static void MapRecommendationEndpoints(this WebApplication app)
    {
        app.MapGet("/api/recommendations/{topicId:int}", async (int topicId, AppDbContext db) =>
        {
            Topic? topic = await db.Topics
                .Include(t => t.Subject)
                .Include(t => t.Resources)
                .FirstOrDefaultAsync(t => t.Id == topicId);

            if (topic == null)
                return Results.Problem($"No se encontró el tema con ID {topicId}.", statusCode: 404);

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
