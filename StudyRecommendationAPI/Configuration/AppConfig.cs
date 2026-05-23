namespace StudyRecommendationAPI.Configuration;

public class ExternalApisConfig
{
    public const string Section = "ExternalApis";

    public YouTubeConfig YouTube { get; set; } = new();
    public BraveSearchConfig BraveSearch { get; set; } = new();
    public ClaudeCodeConfig ClaudeCode { get; set; } = new();
}

public class YouTubeConfig
{
    public string ApiKey { get; set; } = string.Empty;
    public int MaxResultsPerTopic { get; set; } = 2;
    public bool IsEnabled => !string.IsNullOrWhiteSpace(ApiKey);
}

public class BraveSearchConfig
{
    public string ApiKey { get; set; } = string.Empty;
    public int MaxResultsPerTopic { get; set; } = 2;
    public bool IsEnabled => !string.IsNullOrWhiteSpace(ApiKey);
}

public class ClaudeCodeConfig
{
    public string ExecutablePath { get; set; } = "claude";
    public int TimeoutSeconds { get; set; } = 120;
}
