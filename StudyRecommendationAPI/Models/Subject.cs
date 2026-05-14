namespace StudyRecommendationAPI.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Career { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public List<Topic> Topics { get; set; } = new();
}
