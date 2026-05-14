using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;

namespace StudyRecommendationAPI.Extensions;

public static class SyllabusEndpoints
{
    public static void MapSyllabusEndpoints(this WebApplication app)
    {
        app.MapPost("/api/syllabus/process", async (ProcessSyllabusRequest request, AppDbContext db) =>
        {
            if (string.IsNullOrWhiteSpace(request.SubjectName))
                return Results.Problem("El nombre de la materia es requerido.", statusCode: 400);

            Subject? existing = await db.Subjects
                .Include(s => s.Topics)
                .FirstOrDefaultAsync(s => s.Name == request.SubjectName);

            if (existing != null)
                return Results.Ok(BuildSyllabusResponse(existing));

            Subject subject = new()
            {
                Name = request.SubjectName,
                Career = request.Career,
                Institution = request.Institution,
                CreatedAt = DateTime.UtcNow
            };
            db.Subjects.Add(subject);
            await db.SaveChangesAsync();

            Dictionary<string, List<(int unitNumber, string unitName, string topicName, int orderIndex)>> mockData =
                SeedData.GetMockTopics();

            List<(int unitNumber, string unitName, string topicName, int orderIndex)> topicDefs =
                mockData.TryGetValue(request.SubjectName, out List<(int, string, string, int)>? found)
                    ? found
                    : GenerateGenericTopics();

            List<Topic> topics = topicDefs.Select(t => new Topic
            {
                SubjectId = subject.Id,
                UnitNumber = t.unitNumber,
                UnitName = t.unitName,
                TopicName = t.topicName,
                OrderIndex = t.orderIndex
            }).ToList();

            db.Topics.AddRange(topics);
            await db.SaveChangesAsync();

            subject.Topics = topics;
            return Results.Ok(BuildSyllabusResponse(subject));
        })
        .WithName("ProcessSyllabus")
        .WithTags("Syllabus")
        .WithSummary("Procesa un syllabus y devuelve los temas estructurados")
        .Produces<ProcessSyllabusResponse>()
        .ProducesProblem(400);
    }

    private static ProcessSyllabusResponse BuildSyllabusResponse(Subject subject)
    {
        List<UnitDto> units = subject.Topics
            .GroupBy(t => new { t.UnitNumber, t.UnitName })
            .OrderBy(g => g.Key.UnitNumber)
            .Select(g => new UnitDto
            {
                UnitNumber = g.Key.UnitNumber,
                UnitName = g.Key.UnitName,
                Topics = g.OrderBy(t => t.OrderIndex).Select(t => new TopicSummaryDto
                {
                    TopicId = t.Id,
                    TopicName = t.TopicName,
                    OrderIndex = t.OrderIndex
                }).ToList()
            }).ToList();

        return new ProcessSyllabusResponse
        {
            SubjectId = subject.Id,
            SubjectName = subject.Name,
            Units = units
        };
    }

    private static List<(int, string, string, int)> GenerateGenericTopics()
    {
        return new List<(int, string, string, int)>
        {
            (1, "Introducción", "Conceptos fundamentales", 1),
            (1, "Introducción", "Historia y evolución", 2),
            (2, "Desarrollo", "Principios y metodologías", 1),
            (2, "Desarrollo", "Aplicaciones prácticas", 2),
            (3, "Avanzado", "Tópicos avanzados", 1),
            (3, "Avanzado", "Casos de estudio", 2),
        };
    }
}
