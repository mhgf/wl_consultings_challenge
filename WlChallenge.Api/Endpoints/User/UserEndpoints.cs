using WlChallenge.Api.Endpoints.User.Command;
using WlChallenge.Api.Endpoints.User.Query;
using WlChallenge.Api.Endpoints.User.Query.Dtos;
using WlChallenge.Api.Shared.Response;
using AddAmountResponse = WlChallenge.Application.UseCases.User.AddBalance.Response;

namespace WlChallenge.Api.Endpoints.User;

public static class UserEndpoints
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users")
            .WithTags("Users")
            .RequireAuthorization();

        group.MapGet("/", GetAllUser.Execute)
            .WithDescription("Lista todos os usuários.")
            .Produces<Response<PaginationResponse<UserDto>>>()
            .Produces<Response>(400);

        group.MapPost("/", Create.Execute)
            .AllowAnonymous()
            .WithDescription("Cria um novo usuário.")
            .Produces<Response<Guid>>()
            .Produces<Response>(400);


        var walletGroup = group.MapGroup("/my-wallet");


        walletGroup.MapGet("/", GetBalance.Execute)
            .Produces<Response<BalanceDto>>();
        walletGroup.MapGet("/transactions", GetTransactions.Execute)
            .Produces<Response<PaginationResponse<TransactionDto>>>();

        walletGroup.MapPost("/balance", AddBalance.Execute)
            .WithDescription("Adiciona saldo a carteira do usuário logado.")
            .Produces<Response<AddAmountResponse>>()
            .Produces<Response>(400);

        walletGroup.MapPost("/transfer", Transfer.Execute)
            .WithDescription("Realiza um transferência entre usuários.")
            .Produces<Response>(400);
    }
}