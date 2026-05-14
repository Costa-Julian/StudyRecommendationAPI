namespace StudyRecommendationAPI.Models;

public class Resource
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public int PositiveVotes { get; set; }
    public int NegativeVotes { get; set; }
    public DateTime CreatedAt { get; set; }

    public Topic Topic { get; set; } = null!;
    public List<Feedback> Feedbacks { get; set; } = new();
}
