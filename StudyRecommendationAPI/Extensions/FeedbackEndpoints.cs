using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;

namespace StudyRecommendationAPI.Extensions;

public static class FeedbackEndpoints
{
    public static void MapFeedbackEndpoints(this WebApplication app)
    {
        app.MapPost("/api/feedback", async (SubmitFeedbackRequest request, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return Results.Problem("El userId es requerido.", statusCode: 400);

            Resource? resource = await db.Resources.FindAsync(request.ResourceId);
            if (resource == null)
                return Results.Problem($"No se encontró el recurso con ID {request.ResourceId}.", statusCode: 404);

            bool alreadyVoted = await db.Feedbacks
                .AnyAsync(f => f.ResourceId == request.ResourceId && f.UserId == request.UserId);

            if (alreadyVoted)
                return Results.Problem(
                    $"El usuario '{request.UserId}' ya registró feedback para el recurso {request.ResourceId}.",
                    statusCode: 409);

            Feedback feedback = new()
            {
                ResourceId = request.ResourceId,
                UserId = request.UserId,
                IsHelpful = request.IsHelpful,
                CreatedAt = DateTime.UtcNow
            };
            db.Feedbacks.Add(feedback);

            if (request.IsHelpful)
                resource.PositiveVotes++;
            else
                resource.NegativeVotes++;

            await db.SaveChangesAsync();

            int total = resource.PositiveVotes + resource.NegativeVotes;
            double newRating = total == 0 ? 0 : Math.Round((double)resource.PositiveVotes / total, 2);

            SubmitFeedbackResponse response = new()
            {
                Message = "Feedback registrado",
                ResourceId = resource.Id,
                NewRating = newRating,
                TotalVotes = total
            };

            return Results.Ok(response);
        })
        .WithName("SubmitFeedback")
        .WithTags("Feedback")
        .WithSummary("Registra el feedback de un estudiante sobre un recurso")
        .Produces<SubmitFeedbackResponse>()
        .ProducesProblem(400)
        .ProducesProblem(404)
        .ProducesProblem(409);
    }
}
