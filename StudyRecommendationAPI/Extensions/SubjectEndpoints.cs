using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;

namespace StudyRecommendationAPI.Extensions;

public static class SubjectEndpoints
{
    public static void MapSubjectEndpoints(this WebApplication app)
    {
        app.MapGet("/api/subjects", async (AppDbContext db) =>
        {
            List<SubjectSummaryDto> result = await db.Subjects
                .Select(s => new SubjectSummaryDto
                {
                    SubjectId = s.Id,
                    Name = s.Name,
                    Career = s.Career,
                    Institution = s.Institution,
                    TotalTopics = s.Topics.Count,
                    TotalResources = s.Topics.SelectMany(t => t.Resources).Count()
                })
                .ToListAsync();

            return Results.Ok(result);
        })
        .WithName("GetSubjects")
        .WithTags("Materias")
        .WithSummary("Devuelve todas las materias con cantidad de temas y recursos")
        .Produces<List<SubjectSummaryDto>>();

        app.MapGet("/api/subjects/{subjectId:int}/topics", async (int subjectId, AppDbContext db) =>
        {
            Subject? subject = await db.Subjects
                .Include(s => s.Topics)
                    .ThenInclude(t => t.Resources)
                .FirstOrDefaultAsync(s => s.Id == subjectId);

            if (subject == null)
                return Results.Problem($"No se encontró la materia con ID {subjectId}.", statusCode: 404);

            List<UnitWithResourcesDto> units = subject.Topics
                .GroupBy(t => new { t.UnitNumber, t.UnitName })
                .OrderBy(g => g.Key.UnitNumber)
                .Select(g => new UnitWithResourcesDto
                {
                    UnitNumber = g.Key.UnitNumber,
                    UnitName = g.Key.UnitName,
                    Topics = g.OrderBy(t => t.OrderIndex).Select(t => new TopicWithResourceCountDto
                    {
                        TopicId = t.Id,
                        TopicName = t.TopicName,
                        OrderIndex = t.OrderIndex,
                        ResourceCount = t.Resources.Count
                    }).ToList()
                }).ToList();

            SubjectTopicsResponse response = new()
            {
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                Units = units
            };

            return Results.Ok(response);
        })
        .WithName("GetSubjectTopics")
        .WithTags("Materias")
        .WithSummary("Devuelve los temas de una materia organizados por unidad")
        .Produces<SubjectTopicsResponse>()
        .ProducesProblem(404);
    }
}
