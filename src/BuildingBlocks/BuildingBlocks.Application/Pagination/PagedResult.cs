namespace BuildingBlocks.Application.Pagination;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; }
    public int PageSize { get; }
    public int PageNumber { get; }
    public int TotalCount { get; }
    public PagedResult(IEnumerable<T> items, int pageSize, int pageNumber, int totalCount)
    {
        Items = items;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalCount = totalCount;
    }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}
