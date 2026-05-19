using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using StudyRecommendationAPI.Configuration;

namespace StudyRecommendationAPI.Services;

public class YouTubeService(IHttpClientFactory httpFactory, IOptions<ExternalApisConfig> config)
{
    private readonly YouTubeConfig _config = config.Value.YouTube;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public bool IsEnabled => _config.IsEnabled;

    public async Task<List<ResourceSuggestion>> SearchVideosAsync(string topicName, string subjectName)
    {
        try
        {
            string query = Uri.EscapeDataString($"{topicName} {subjectName} tutorial");
            string url = $"https://www.googleapis.com/youtube/v3/search"
                       + $"?part=snippet&q={query}&type=video"
                       + $"&maxResults={_config.MaxResultsPerTopic}"
                       + $"&relevanceLanguage=es"
                       + $"&key={_config.ApiKey}";

            using HttpClient http = httpFactory.CreateClient();
            using HttpResponseMessage response = await http.GetAsync(url);
            if (!response.IsSuccessStatusCode) return [];

            string raw = await response.Content.ReadAsStringAsync();
            YouTubeSearchResponse? result = JsonSerializer.Deserialize<YouTubeSearchResponse>(raw, JsonOptions);

            if (result?.Items == null || result.Items.Count == 0) return [];

            return result.Items
                .Where(item => item.Id?.VideoId != null)
                .Select(item => new ResourceSuggestion
                {
                    Type = "video",
                    Title = item.Snippet?.Title ?? topicName,
                    Url = $"https://www.youtube.com/watch?v={item.Id!.VideoId}",
                    Source = item.Snippet?.ChannelTitle ?? "YouTube"
                })
                .ToList();
        }
        catch
        {
            return [];
        }
    }
}

file class YouTubeSearchResponse
{
    [JsonPropertyName("items")]
    public List<YouTubeItem> Items { get; set; } = [];
}

file class YouTubeItem
{
    [JsonPropertyName("id")]
    public YouTubeItemId? Id { get; set; }

    [JsonPropertyName("snippet")]
    public YouTubeSnippet? Snippet { get; set; }
}

file class YouTubeItemId
{
    [JsonPropertyName("videoId")]
    public string? VideoId { get; set; }
}

file class YouTubeSnippet
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("channelTitle")]
    public string? ChannelTitle { get; set; }
}
