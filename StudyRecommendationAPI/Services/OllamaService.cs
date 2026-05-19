using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using StudyRecommendationAPI.Configuration;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace StudyRecommendationAPI.Services;

public class OllamaService(IHttpClientFactory httpFactory, IOptions<ExternalApisConfig> config)
{
    private readonly OllamaConfig _config = config.Value.Ollama;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<List<(int unitNumber, string unitName, string topicName, int orderIndex)>?>
        ExtractSyllabusTopicsAsync(string subjectName, string? fileBase64 = null)
    {
        try
        {
            string? pdfText = null;
            if (!string.IsNullOrEmpty(fileBase64) && fileBase64.StartsWith("JVBERi0"))
                pdfText = ExtractPdfText(fileBase64);

            using HttpClient http = httpFactory.CreateClient("ollama");
            http.Timeout = TimeSpan.FromSeconds(_config.TimeoutSeconds);

            string prompt = BuildSyllabusPrompt(subjectName, pdfText);

            object body = new
            {
                model = _config.Model,
                prompt = prompt,
                stream = false
            };

            using HttpRequestMessage request = new(HttpMethod.Post, "/api/generate")
            {
                Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
            };

            using HttpResponseMessage response = await http.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;

            string raw = await response.Content.ReadAsStringAsync();
            OllamaGenerateResponse? ollamaResponse = JsonSerializer.Deserialize<OllamaGenerateResponse>(raw, JsonOptions);
            string? text = ollamaResponse?.Response;

            if (string.IsNullOrEmpty(text)) return null;
            return ParseSyllabusJson(text);
        }
        catch
        {
            return null;
        }
    }

    private static string? ExtractPdfText(string base64)
    {
        try
        {
            byte[] bytes = Convert.FromBase64String(base64);
            using PdfDocument pdf = PdfDocument.Open(bytes);
            StringBuilder sb = new();
            foreach (Page page in pdf.GetPages())
                sb.AppendLine(page.Text);
            return sb.ToString();
        }
        catch { return null; }
    }

    private static string BuildSyllabusPrompt(string subjectName, string? pdfText)
    {
        string jsonExample = """{"units":[{"unitNumber":1,"unitName":"Nombre Unidad","topics":[{"topicName":"Tema específico","orderIndex":1}]}]}""";

        if (!string.IsNullOrWhiteSpace(pdfText))
        {
            string truncated = pdfText.Length > 4000 ? pdfText[..4000] : pdfText;
            return $"""
                Analizá el siguiente programa de la materia "{subjectName}" y extraé su estructura de unidades y temas.
                Devolvé ÚNICAMENTE el JSON sin markdown ni explicaciones:
                {jsonExample}

                Programa:
                {truncated}
                """;
        }

        return $"""
            Sos un experto en diseño curricular universitario.
            Generá un programa académico completo para la materia "{subjectName}" con 4 a 6 unidades temáticas.
            Devolvé ÚNICAMENTE el JSON sin markdown ni explicaciones.
            Cada unidad debe tener entre 3 y 6 temas específicos y educativos.
            Usá el idioma del nombre de la materia.
            Formato JSON requerido:
            {jsonExample}
            """;
    }

    private static List<(int, string, string, int)>? ParseSyllabusJson(string text)
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

            int jsonStart = text.IndexOf('{');
            if (jsonStart > 0) text = text[jsonStart..];

            SyllabusResult? result = JsonSerializer.Deserialize<SyllabusResult>(text, JsonOptions);
            if (result?.Units == null || result.Units.Count == 0) return null;

            return result.Units
                .SelectMany(u => u.Topics.Select((t, i) =>
                    (u.UnitNumber, u.UnitName, t.TopicName, t.OrderIndex > 0 ? t.OrderIndex : i + 1)))
                .ToList();
        }
        catch { return null; }
    }
}

file class OllamaGenerateResponse
{
    [JsonPropertyName("response")]
    public string Response { get; set; } = string.Empty;
}
