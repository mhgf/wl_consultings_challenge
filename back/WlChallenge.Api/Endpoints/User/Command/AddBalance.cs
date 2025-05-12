using MediatR;
using WlChallenge.Api.Endpoints.User.Command.Dtos;
using WlChallenge.Api.Extensions;
using WlChallenge.Api.Shared.Response;
using CommandType = WlChallenge.Application.UseCases.User.AddBalance.Command;

namespace WlChallenge.Api.Endpoints.User.Command;

public static class AddBalance
{
    public static async Task<IResult> Execute(AmountDto amountDto, IMediator mediator, HttpContext ctx,
        CancellationToken cancellationToken)
    {
        var userId = ctx.User.GetUserId();

        var result = await mediator.Send(new CommandType(userId, amountDto.Amount), cancellationToken);

        if (result.IsSuccess) return Results.Ok(Response.Ok(result.Value));

        return Results.BadRequest(Response.Fail([result.Error]));
    }
}