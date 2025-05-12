namespace WlChallenge.Api.Shared.Response;

public class PaginationResponse<T>(IEnumerable<T> items, int page, int pageSize, int totalCount)
{
    public IEnumerable<T> Items { get; } = items;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public int TotalPages { get; } = (int)Math.Ceiling(totalCount / (double)pageSize);

    public IResult ToResult()
        => Results.Ok(Response.Ok(this));


    public static PaginationResponse<TList> Create<TList>(IEnumerable<TList> items, int pageNumber, int pageSize, int totalCount)
        => new(items, pageNumber, pageSize, totalCount);
}