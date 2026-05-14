namespace StudyRecommendationAPI.Models;

public class Feedback
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool IsHelpful { get; set; }
    public DateTime CreatedAt { get; set; }

    public Resource Resource { get; set; } = null!;
}
