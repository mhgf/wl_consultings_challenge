using WlChallenge.Application.UseCases.Abstractions;

namespace WlChallenge.Application.UseCases.User.AddBalance;

public sealed record Command(Guid UserId, int Amount) : ICommand<Response>;