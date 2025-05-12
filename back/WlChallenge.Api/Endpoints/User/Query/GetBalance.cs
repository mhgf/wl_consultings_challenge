using Microsoft.EntityFrameworkCore;
using WlChallenge.Api.Endpoints.User.Query.Dtos;
using WlChallenge.Api.Extensions;
using WlChallenge.Api.Shared.Response;
using WlChallenge.Infra.Data;

namespace WlChallenge.Api.Endpoints.User.Query;

public static class GetBalance
{
    public static async Task<IResult> Execute(AppDbContext dbContext, HttpContext context,
        CancellationToken cancellationToken)
    {
        var userId = context.User.GetUserId();

        var balance = await dbContext
            .Users
            .Where(x => x.Id == userId)
            .Select(x => new BalanceDto(x.Wallet.Balance, x.Wallet.Tracker.UpdatedAtUtc))
            .FirstOrDefaultAsync(cancellationToken);

        return Results.Ok(Response.Ok(balance));
    }
}