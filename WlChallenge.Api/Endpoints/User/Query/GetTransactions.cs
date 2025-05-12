using Microsoft.EntityFrameworkCore;
using WlChallenge.Api.Endpoints.User.Query.Dtos;
using WlChallenge.Api.Extensions;
using WlChallenge.Api.Shared.Response;
using WlChallenge.Infra.Data;

namespace WlChallenge.Api.Endpoints.User.Query;

public static class GetTransactions
{
    public static async Task<IResult> Execute(
        [AsParameters] TransactionRequest request,
        AppDbContext dbContext,
        HttpContext ctx,
        CancellationToken cancellationToken)
    {
        var userId = ctx.User.GetUserId();

        var query = dbContext
            .Transactions
            .AsNoTracking()
            .Where(t => t.Wallet.UserId == userId);

        if (request.StartDate is not null)
        {
            query = query.Where(x => x.Tracker.CreatedAtUtc.Date >= request.StartDate.Value.Date);
            if (request.EndDate is not null)
                query = query.Where(x => x.Tracker.CreatedAtUtc.Date <= request.EndDate.Value.Date);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var transactions = await query
            .Take(request.PageSize)
            .Skip((request.Page - 1) * request.PageSize)
            .OrderByDescending(x => x.Tracker.CreatedAtUtc)
            .Select(x => new TransactionDto(
                x.SenderId,
                x.Sender.Name,
                x.ReceiverId,
                x.Receiver.Name,
                x.Amount,
                x.Type,
                x.Tracker.CreatedAtUtc))
            .ToListAsync(cancellationToken);

        return PaginationResponse<TransactionDto>
            .Create(transactions, request.Page, request.PageSize, totalCount)
            .ToResult();
    }
}