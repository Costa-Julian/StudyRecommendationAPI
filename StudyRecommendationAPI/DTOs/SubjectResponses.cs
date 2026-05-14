namespace StudyRecommendationAPI.DTOs;

public class SubjectSummaryDto
{
    public int SubjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Career { get; set; } = string.Empty;
    public string Institution { get; set; } = string.Empty;
    public int TotalTopics { get; set; }
    public int TotalResources { get; set; }
}

public class SubjectTopicsResponse
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public List<UnitWithResourcesDto> Units { get; set; } = new();
}

public class UnitWithResourcesDto
{
    public int UnitNumber { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public List<TopicWithResourceCountDto> Topics { get; set; } = new();
}

public class TopicWithResourceCountDto
{
    public int TopicId { get; set; }
    public string TopicName { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int ResourceCount { get; set; }
}
