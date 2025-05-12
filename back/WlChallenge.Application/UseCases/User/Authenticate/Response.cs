using WlChallenge.Application.UseCases.Abstractions;

namespace WlChallenge.Application.UseCases.User.Authenticate;

public record Response(Guid UserId, string UserName, string Email) : ICommandResponse;