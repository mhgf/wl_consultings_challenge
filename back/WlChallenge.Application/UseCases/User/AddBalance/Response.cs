using WlChallenge.Application.UseCases.Abstractions;

namespace WlChallenge.Application.UseCases.User.AddBalance;

public record Response(int Balance) : ICommandResponse;