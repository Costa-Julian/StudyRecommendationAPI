using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using StudyRecommendationAPI.Data;
using StudyRecommendationAPI.DTOs;
using StudyRecommendationAPI.Models;
using StudyRecommendationAPI.Services;

namespace StudyRecommendationAPI.Extensions;

public static class SyllabusEndpoints
{
    public static void MapSyllabusEndpoints(this WebApplication app)
    {
        app.MapPost("/api/syllabus/process", async (ProcessSyllabusRequest request, AppDbContext db, OllamaService ollama, ClaudeService claude) =>
        {
            if (string.IsNullOrWhiteSpace(request.SubjectName))
                return Results.Problem("El nombre de la materia es requerido.", statusCode: 400);

            string subjectName = request.SubjectName.Trim();

            var allSubjects = await db.Subjects.Select(s => new { s.Id, s.Name }).ToListAsync();
            string normalizedInput = NormalizeForMatch(subjectName);
            var nameMatch = allSubjects
                .Select(s => new { s.Id, s.Name, Score = WordMatchScore(normalizedInput, NormalizeForMatch(s.Name.Trim())) })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .ThenByDescending(x => x.Name.Length)
                .FirstOrDefault();

            Subject? existing = nameMatch == null ? null : await db.Subjects
                .Include(s => s.Topics)
                .FirstOrDefaultAsync(s => s.Id == nameMatch.Id);

            if (existing != null)
                return Results.Ok(BuildSyllabusResponse(existing));

            Subject subject = new()
            {
                Name = subjectName,
                Career = request.Career,
                Institution = request.Institution,
                CreatedAt = DateTime.UtcNow
            };
            db.Subjects.Add(subject);
            await db.SaveChangesAsync();

            // First check mocked topics for known subjects, then fall back to Claude
            Dictionary<string, List<(int unitNumber, string unitName, string topicName, int orderIndex)>> mockData =
                SeedData.GetMockTopics();

            List<(int unitNumber, string unitName, string topicName, int orderIndex)> topicDefs;

            if (mockData.TryGetValue(subjectName, out List<(int, string, string, int)>? found))
            {
                topicDefs = found;
            }
            else
            {
                // Try Ollama first (local, free); fall back to Claude if unavailable
                List<(int, string, string, int)>? ollamaTopics =
                    await ollama.ExtractSyllabusTopicsAsync(subjectName, request.FileBase64);

                topicDefs = (ollamaTopics != null && ollamaTopics.Count > 0)
                    ? ollamaTopics
                    : await claude.ExtractSyllabusTopicsAsync(request.FileBase64, subjectName);
            }

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

    private static string NormalizeForMatch(string text)
    {
        // Remove diacritics and lowercase
        string normalized = new string(
            text.ToLowerInvariant()
                .Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray()
        ).Normalize(NormalizationForm.FormC);

        // Convert Roman numerals to Arabic so "II" == "2", "III" == "3", etc.
        normalized = Regex.Replace(normalized, @"\bxii\b", "12");
        normalized = Regex.Replace(normalized, @"\bxi\b", "11");
        normalized = Regex.Replace(normalized, @"\bviii\b", "8");
        normalized = Regex.Replace(normalized, @"\bvii\b", "7");
        normalized = Regex.Replace(normalized, @"\bvi\b", "6");
        normalized = Regex.Replace(normalized, @"\biv\b", "4");
        normalized = Regex.Replace(normalized, @"\biii\b", "3");
        normalized = Regex.Replace(normalized, @"\bii\b", "2");

        return normalized;
    }

    // Score how many words from the input match words in the subject (prefix match for abbreviations)
    private static int WordMatchScore(string normalizedInput, string normalizedSubject)
    {
        string[] inputWords = normalizedInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] subjectWords = normalizedSubject.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return inputWords.Count(iw =>
            iw.Length <= 2
                ? subjectWords.Any(sw => sw == iw)
                : subjectWords.Any(sw => sw.StartsWith(iw) || iw.StartsWith(sw))
        );
    }

    private static string RemoveDiacritics(string text)
    {
        string normalized = text.Normalize(NormalizationForm.FormD);
        var chars = normalized.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);
        return new string(chars.ToArray()).Normalize(NormalizationForm.FormC);
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
