using WlChallenge.Application.UseCases.Abstractions;
using WlChallenge.Domain.Data.Abstractions;
using WlChallenge.Domain.Repositories;
using WlChallenge.Domain.Results;

namespace WlChallenge.Application.UseCases.User.MakeTransfer;

public class Handler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<Command>
{
    public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
    {
        var sender = await userRepository.FindById(request.SenderId, cancellationToken);
        if (sender is null)
            return Result.Failure("Usuário não encontrado.");

        var receiver = await userRepository.FindById(request.ReceiverId, cancellationToken);
        if (receiver is null)
            return Result.Failure("Usuário não encontrado.");

        if (sender.Balance < request.Amount)
            return Result.Failure("Saldo insuficiente.");

        sender.SendAmountToUser(request.ReceiverId, request.Amount);
        receiver.ReceiveAmountFromUser(request.SenderId, request.Amount);

        userRepository.Update(sender, cancellationToken);
        userRepository.Update(receiver, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
        return Result.Ok();
    }
}