using WlChallenge.Application.UseCases.Abstractions;

namespace WlChallenge.Application.UseCases.User.MakeTransfer;

public sealed record Command(Guid SenderId, Guid ReceiverId, int Amount) : ICommand;