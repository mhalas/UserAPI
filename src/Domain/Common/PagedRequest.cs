namespace Domain.Common
{
    public class PagedRequest
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SortBy { get; init; } = string.Empty;
        public bool IsDescending { get; set; } = false;
        public IDictionary<string, string> FilterBy { get; set; } = new Dictionary<string, string>();
    }
}
