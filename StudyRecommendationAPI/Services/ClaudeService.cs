using System.Text.Json;
using System.Text.Json.Serialization;
using Anthropic;
using Anthropic.Models.Messages;

namespace StudyRecommendationAPI.Services;

public class ClaudeService
{
    private readonly AnthropicClient _client;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ClaudeService(string apiKey)
    {
        _client = new AnthropicClient { ApiKey = apiKey };
    }

    // Extracts structured topics from a syllabus PDF, or generates a curriculum by subject name
    public async Task<List<(int unitNumber, string unitName, string topicName, int orderIndex)>>
        ExtractSyllabusTopicsAsync(string fileBase64, string subjectName)
    {
        try
        {
            bool isPdf = !string.IsNullOrEmpty(fileBase64) && fileBase64.StartsWith("JVBERi0");

            List<ContentBlockParam> blocks = new();

            if (isPdf)
                blocks.Add(new DocumentBlockParam { Source = new Base64PdfSource { Data = fileBase64 } });

            blocks.Add(new TextBlockParam { Text = BuildSyllabusPrompt(subjectName, isPdf) });

            var response = await _client.Messages.Create(new MessageCreateParams
            {
                Model = "claude-opus-4-7",
                MaxTokens = 4096,
                Messages = [new MessageParam { Role = Role.User, Content = blocks }]
            });

            string? text = response.Content.Select(b => b.Value).OfType<TextBlock>().FirstOrDefault()?.Text;
            if (string.IsNullOrEmpty(text)) return FallbackTopics();

            SyllabusResult? result = ParseJson<SyllabusResult>(text);
            if (result?.Units == null || result.Units.Count == 0) return FallbackTopics();

            return result.Units
                .SelectMany(u => u.Topics.Select((t, i) =>
                    (u.UnitNumber, u.UnitName, t.TopicName, t.OrderIndex > 0 ? t.OrderIndex : i + 1)))
                .ToList();
        }
        catch
        {
            return FallbackTopics();
        }
    }

    // Asks Claude for educational resource suggestions when none exist in the DB for a topic
    public async Task<List<ResourceSuggestion>> GetResourceRecommendationsAsync(string topicName, string subjectName)
    {
        try
        {
            var response = await _client.Messages.Create(new MessageCreateParams
            {
                Model = "claude-opus-4-7",
                MaxTokens = 2048,
                Messages = [new MessageParam
                {
                    Role = Role.User,
                    Content = BuildResourcePrompt(topicName, subjectName)
                }]
            });

            string? text = response.Content.Select(b => b.Value).OfType<TextBlock>().FirstOrDefault()?.Text;
            if (string.IsNullOrEmpty(text)) return [];
            return ParseJson<List<ResourceSuggestion>>(text) ?? [];
        }
        catch
        {
            return [];
        }
    }

    private static string BuildSyllabusPrompt(string subjectName, bool hasPdf)
    {
        string jsonExample = """{"units":[{"unitNumber":1,"unitName":"Unit Name","topics":[{"topicName":"Specific Topic","orderIndex":1}]}]}""";

        return hasPdf
            ? $"""
               Analyze the attached syllabus for "{subjectName}". Extract the course structure.
               Return ONLY this JSON (no markdown, no explanation):
               {jsonExample}
               Rules: each unit must have 3-6 topics with specific, university-level topic names in the same language as the document.
               """
            : $"""
               Generate a university-level curriculum for the course "{subjectName}" with 4-6 thematic units.
               Return ONLY this JSON (no markdown, no explanation):
               {jsonExample}
               Rules: each unit must have 3-6 specific, educational topics. Use Spanish if the subject name is in Spanish.
               """;
    }

    private static string BuildResourcePrompt(string topicName, string subjectName)
    {
        string jsonExample = """[{"type":"video","title":"Resource Title","url":"https://example.com","source":"YouTube"}]""";

        return $"""
                Suggest 4 educational resources for the topic "{topicName}" in the course "{subjectName}".
                Return ONLY this JSON array (no markdown, no explanation):
                {jsonExample}
                Rules:
                - type must be one of: "video", "article", "book"
                - Include at least 2 videos and 1 article
                - source examples: YouTube, Khan Academy, MIT OCW, Coursera, Medium, freeCodeCamp
                - URLs should point to real educational platforms
                - Titles must be specific to the topic
                """;
    }

    private static T? ParseJson<T>(string text)
    {
        try
        {
            text = text.Trim();
            if (text.StartsWith("```"))
            {
                int start = text.IndexOf('\n') + 1;
                int end = text.LastIndexOf("```");
                if (end > start) text = text[start..end].Trim();
            }
            return JsonSerializer.Deserialize<T>(text, JsonOptions);
        }
        catch { return default; }
    }

    private static List<(int, string, string, int)> FallbackTopics() =>
    [
        (1, "Introducción", "Conceptos fundamentales", 1),
        (1, "Introducción", "Historia y evolución", 2),
        (2, "Desarrollo", "Principios y metodologías", 1),
        (2, "Desarrollo", "Aplicaciones prácticas", 2),
        (3, "Avanzado", "Tópicos avanzados", 1),
        (3, "Avanzado", "Casos de estudio", 2),
    ];
}

// DTOs for Claude JSON responses
public class SyllabusResult
{
    [JsonPropertyName("units")]
    public List<SyllabusUnit> Units { get; set; } = [];
}

public class SyllabusUnit
{
    [JsonPropertyName("unitNumber")]
    public int UnitNumber { get; set; }

    [JsonPropertyName("unitName")]
    public string UnitName { get; set; } = string.Empty;

    [JsonPropertyName("topics")]
    public List<SyllabusTopic> Topics { get; set; } = [];
}

public class SyllabusTopic
{
    [JsonPropertyName("topicName")]
    public string TopicName { get; set; } = string.Empty;

    [JsonPropertyName("orderIndex")]
    public int OrderIndex { get; set; }
}

public class ResourceSuggestion
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;
}
