using MediatR;
using WlChallenge.Api.Endpoints.User.Command.Dtos;
using WlChallenge.Api.Extensions;
using WlChallenge.Api.Shared.Response;
using CommandType = WlChallenge.Application.UseCases.User.MakeTransfer.Command;

namespace WlChallenge.Api.Endpoints.User.Command;

public static class Transfer
{
    public static async Task<IResult> Execute(MakeTransferDto dto, IMediator mediator, HttpContext ctx,
        CancellationToken cancellationToken)
    {
        var userId = ctx.User.GetUserId();

        var result = await mediator.Send(new CommandType(userId, dto.Receiver, dto.Amount), cancellationToken);

        return result.IsSuccess ? Results.Ok() : Results.BadRequest(Response.Fail([result.Error]));
    }
}