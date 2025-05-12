using MediatR;
using WlChallenge.Api.Endpoints.Auth.Command.Dtos;
using WlChallenge.Api.Extensions;
using WlChallenge.Api.Shared.Response;
using CommandType = WlChallenge.Application.UseCases.User.Authenticate.Command;

namespace WlChallenge.Api.Endpoints.Auth.Command;

public static class Authenticate
{
    public static async Task<IResult> Execute(CommandType command, IMediator mediator)
    {
        var result = await mediator.Send(command);

        return result.IsSuccess
            ? Results.Ok(Response.Ok(new TokenDto(result.Value.GenerateToken())))
            : Results.BadRequest(Response.Fail([result.Error]));
    }
}