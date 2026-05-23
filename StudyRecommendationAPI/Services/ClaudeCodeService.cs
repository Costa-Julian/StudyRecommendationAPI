using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using StudyRecommendationAPI.Configuration;
using StudyRecommendationAPI.DTOs;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace StudyRecommendationAPI.Services;

public class ClaudeCodeService(IOptions<ExternalApisConfig> config)
{
    private readonly ClaudeCodeConfig _config = config.Value.ClaudeCode;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<(bool Success, string Result, string Error)> RunPromptAsync(
        string prompt, string[]? allowedTools = null)
    {
        ProcessStartInfo startInfo = BuildStartInfo(prompt, allowedTools);

        using Process process = new() { StartInfo = startInfo };

        try
        {
            process.Start();
            process.StandardInput.Close();

            using CancellationTokenSource cts = new(TimeSpan.FromSeconds(_config.TimeoutSeconds));

            Task<string> stdoutTask = process.StandardOutput.ReadToEndAsync(cts.Token);
            Task<string> stderrTask = process.StandardError.ReadToEndAsync(cts.Token);

            await process.WaitForExitAsync(cts.Token);

            string output = await stdoutTask;
            string error = await stderrTask;

            if (process.ExitCode != 0)
                return (false, string.Empty, string.IsNullOrWhiteSpace(error) ? $"Exit code {process.ExitCode}" : error);

            return (true, output.Trim(), string.Empty);
        }
        catch (OperationCanceledException)
        {
            try { process.Kill(entireProcessTree: true); } catch { /* already exited */ }
            return (false, string.Empty, $"El proceso superó el tiempo límite de {_config.TimeoutSeconds} segundos.");
        }
        catch (Exception ex)
        {
            return (false, string.Empty, ex.Message);
        }
    }

    public async Task<List<(int unitNumber, string unitName, string topicName, int orderIndex)>?>
        ExtractSyllabusTopicsAsync(string subjectName, string? fileBase64 = null)
    {
        try
        {
            string? pdfText = null;
            if (!string.IsNullOrEmpty(fileBase64) && fileBase64.StartsWith("JVBERi0"))
                pdfText = ExtractPdfText(fileBase64);

            string prompt = BuildSyllabusPrompt(subjectName, pdfText);

            (bool success, string result, _) = await RunPromptAsync(prompt);
            if (!success || string.IsNullOrEmpty(result)) return null;

            return ParseSyllabusJson(result);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<ResourceSuggestion>> GetResourceRecommendationsAsync(string topicName, string subjectName)
    {
        try
        {
            string jsonExample = """[{"type":"video","title":"Titulo del recurso","url":"https://ejemplo.com","source":"YouTube"}]""";
            string prompt = $"Usá WebSearch para buscar recursos educativos sobre \"{topicName}\" de la materia \"{subjectName}\". Devolvé ÚNICAMENTE un JSON array sin markdown ni texto extra con este formato: {jsonExample} Incluí al menos 2 videos de YouTube y 1 artículo. El campo type debe ser \"video\" o \"article\".";

            (bool success, string result, _) = await RunPromptAsync(prompt, ["WebSearch"]);
            if (!success || string.IsNullOrEmpty(result)) return [];

            return ParseResourcesJson(result) ?? [];
        }
        catch
        {
            return [];
        }
    }

    private static List<ResourceSuggestion>? ParseResourcesJson(string text)
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
            int jsonStart = text.IndexOf('[');
            if (jsonStart > 0) text = text[jsonStart..];
            return JsonSerializer.Deserialize<List<ResourceSuggestion>>(text, JsonOptions);
        }
        catch { return null; }
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
            return $"Analizá el siguiente programa de la materia \"{subjectName}\" y extraé su estructura de unidades y temas. Devolvé ÚNICAMENTE el JSON sin markdown ni explicaciones: {jsonExample} Programa: {truncated}";
        }

        return $"Sos un experto en diseño curricular universitario. Generá un programa académico completo para la materia \"{subjectName}\" con 4 a 6 unidades temáticas. Devolvé ÚNICAMENTE el JSON sin markdown ni explicaciones. Cada unidad debe tener entre 3 y 6 temas específicos y educativos. Usá el idioma del nombre de la materia. Formato JSON requerido: {jsonExample}";
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

    private ProcessStartInfo BuildStartInfo(string prompt, string[]? allowedTools)
    {
        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        ProcessStartInfo startInfo = new()
        {
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = @"C:\"
        };

        if (isWindows)
        {
            startInfo.FileName = "cmd.exe";
            startInfo.ArgumentList.Add("/c");
            startInfo.ArgumentList.Add(_config.ExecutablePath);
        }
        else
        {
            startInfo.FileName = _config.ExecutablePath;
        }

        startInfo.ArgumentList.Add("-p");
        startInfo.ArgumentList.Add(prompt);
        startInfo.ArgumentList.Add("--output-format");
        startInfo.ArgumentList.Add("text");

        if (allowedTools != null && allowedTools.Length > 0)
        {
            startInfo.ArgumentList.Add("--allowedTools");
            startInfo.ArgumentList.Add(string.Join(",", allowedTools));
        }

        startInfo.ArgumentList.Add("--dangerously-skip-permissions");

        return startInfo;
    }
}

file class SyllabusResult
{
    [JsonPropertyName("units")]
    public List<SyllabusUnit> Units { get; set; } = [];
}

file class SyllabusUnit
{
    [JsonPropertyName("unitNumber")]
    public int UnitNumber { get; set; }

    [JsonPropertyName("unitName")]
    public string UnitName { get; set; } = string.Empty;

    [JsonPropertyName("topics")]
    public List<SyllabusTopic> Topics { get; set; } = [];
}

file class SyllabusTopic
{
    [JsonPropertyName("topicName")]
    public string TopicName { get; set; } = string.Empty;

    [JsonPropertyName("orderIndex")]
    public int OrderIndex { get; set; }
}
