using WlChallenge.Api.Endpoints.Auth.Command;
using WlChallenge.Api.Endpoints.Auth.Command.Dtos;
using WlChallenge.Api.Shared.Response;

namespace WlChallenge.Api.Endpoints.Auth;

public static class AuthEndpoints
{
    public static void AddAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Auth");
        

        group.MapPost("/sing-in", Authenticate.Execute)
            .AllowAnonymous()
            .WithDescription("Authenticate")
            .Produces<Response<TokenDto>>()
            .Produces<Response>(400);

    }
}