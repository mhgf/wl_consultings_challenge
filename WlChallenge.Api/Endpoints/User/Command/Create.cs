using MediatR;
using WlChallenge.Api.Shared.Response;
using CommandType = WlChallenge.Application.UseCases.User.Create.Command;

namespace WlChallenge.Api.Endpoints.User.Command;

public static class Create
{
    public static async Task<IResult> Execute(
        CommandType command, IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/api/users/{result.Value.Id}", Response.Ok(result.Value))
            : Results.BadRequest(Response.Fail([result.Error]));
    }
}