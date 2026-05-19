using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using StudyRecommendationAPI.Configuration;
using StudyRecommendationAPI.DTOs;

namespace StudyRecommendationAPI.Services;

public class BraveSearchService(IHttpClientFactory httpFactory, IOptions<ExternalApisConfig> config)
{
    private readonly BraveSearchConfig _config = config.Value.BraveSearch;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public bool IsEnabled => _config.IsEnabled;

    public async Task<SearchLink?> FindArticleAsync(string topic)
    {
        try
        {
            string query = Uri.EscapeDataString($"{topic} guia articulo tutorial");
            string url = $"https://api.search.brave.com/res/v1/web/search?q={query}&count=1&search_lang=es";

            using HttpClient http = httpFactory.CreateClient();
            http.DefaultRequestHeaders.Add("X-Subscription-Token", _config.ApiKey);
            http.DefaultRequestHeaders.Add("Accept", "application/json");

            using HttpResponseMessage response = await http.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            string raw = await response.Content.ReadAsStringAsync();
            BraveSearchResponse? result = JsonSerializer.Deserialize<BraveSearchResponse>(raw, JsonOptions);

            BraveWebResult? first = result?.Web?.Results?.FirstOrDefault();
            if (first == null) return null;

            return new SearchLink
            {
                Title = first.Title ?? topic,
                Url = first.Url ?? string.Empty,
                Source = ExtractDomain(first.Url)
            };
        }
        catch
        {
            return null;
        }
    }

    private static string ExtractDomain(string? url)
    {
        if (string.IsNullOrEmpty(url)) return "Web";
        try { return new Uri(url).Host.Replace("www.", ""); }
        catch { return "Web"; }
    }
}

file class BraveSearchResponse
{
    [JsonPropertyName("web")]
    public BraveWebSection? Web { get; set; }
}

file class BraveWebSection
{
    [JsonPropertyName("results")]
    public List<BraveWebResult>? Results { get; set; }
}

file class BraveWebResult
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
