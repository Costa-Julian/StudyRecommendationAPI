namespace StudyRecommendationAPI.DTOs;

public class SubmitFeedbackRequest
{
    public int ResourceId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool IsHelpful { get; set; }
}
