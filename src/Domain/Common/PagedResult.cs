namespace Domain.Common
{
    public class PagedResult<TEntity>
    {
        public List<TEntity> Items { get; init; } = [];
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
