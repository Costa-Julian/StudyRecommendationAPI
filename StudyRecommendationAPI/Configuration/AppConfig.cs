namespace StudyRecommendationAPI.Configuration;

public class ExternalApisConfig
{
    public const string Section = "ExternalApis";

    public ClaudeCodeConfig ClaudeCode { get; set; } = new();
}

public class ClaudeCodeConfig
{
    public string ExecutablePath { get; set; } = "claude";
    public int TimeoutSeconds { get; set; } = 120;
}
