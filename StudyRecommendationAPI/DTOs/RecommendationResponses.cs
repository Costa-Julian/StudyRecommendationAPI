namespace StudyRecommendationAPI.DTOs;

public class RecommendationsResponse
{
    public int TopicId { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public List<ResourceDto> Resources { get; set; } = new();
}

public class ResourceDto
{
    public int ResourceId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int TotalVotes { get; set; }
}
