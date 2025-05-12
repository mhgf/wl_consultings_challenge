namespace WlChallenge.Api.Shared.Request;

public class PaginationRequest(int page = Constants.DefaultPageNumber, int pageSize = Constants.DefaultPageSize)
{
    public int Page { get; private set; } = page < 1 ? Constants.DefaultPageNumber : page;

    public int PageSize { get; private set; } = pageSize < 1
        ? Constants.DefaultPageSize
        : pageSize;
}