namespace StudyRecommendationAPI.Models;

public class Topic
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int UnitNumber { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string TopicName { get; set; } = string.Empty;
    public int OrderIndex { get; set; }

    public Subject Subject { get; set; } = null!;
    public List<Resource> Resources { get; set; } = new();
}
