namespace StudyRecommendationAPI.DTOs;

public class ProcessSyllabusResponse
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public List<UnitDto> Units { get; set; } = new();
}

public class UnitDto
{
    public int UnitNumber { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public List<TopicSummaryDto> Topics { get; set; } = new();
}

public class TopicSummaryDto
{
    public int TopicId { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
}
