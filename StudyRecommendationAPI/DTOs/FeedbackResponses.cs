namespace StudyRecommendationAPI.DTOs;

public class SubmitFeedbackResponse
{
    public string Message { get; set; } = string.Empty;
    public int ResourceId { get; set; }
    public double NewRating { get; set; }
    public int TotalVotes { get; set; }
}
