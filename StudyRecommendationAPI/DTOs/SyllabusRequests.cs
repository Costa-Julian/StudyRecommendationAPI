namespace StudyRecommendationAPI.DTOs;

public class ProcessSyllabusRequest
{
    public string SubjectName { get; set; } = string.Empty;
    public string Career { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public string FileBase64 { get; set; } = string.Empty;
}
