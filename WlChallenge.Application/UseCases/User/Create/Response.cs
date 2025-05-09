using WlChallenge.Application.UseCases.Abstractions;

namespace WlChallenge.Application.UseCases.User.Create;

public record Response(Guid Id) : ICommandResponse;