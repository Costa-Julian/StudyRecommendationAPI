namespace StudyRecommendationAPI.Models;

public class SearchResult
{
    public int Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public int VideoPositiveVotes { get; set; }
    public int VideoNegativeVotes { get; set; }
    public string? ArticleUrl { get; set; }
    public int ArticlePositiveVotes { get; set; }
    public int ArticleNegativeVotes { get; set; }
    public DateTime CreatedAt { get; set; }
}
