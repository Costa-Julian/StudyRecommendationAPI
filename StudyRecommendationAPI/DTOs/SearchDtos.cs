namespace StudyRecommendationAPI.DTOs;

public class SearchRequest
{
    public string Topic { get; set; } = string.Empty;
}

public class SearchResponse
{
    public int? Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public bool Success { get; set; }
    public bool FromCache { get; set; }
    public string? Video { get; set; }
    public int VideoPositiveVotes { get; set; }
    public int VideoNegativeVotes { get; set; }
    public string? Article { get; set; }
    public int ArticlePositiveVotes { get; set; }
    public int ArticleNegativeVotes { get; set; }
    public string? Error { get; set; }
}

public class RateSearchRequest
{
    public string ResourceType { get; set; } = string.Empty; // "video" | "article"
    public bool IsPositive { get; set; }
}

public class SearchLink
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
}
