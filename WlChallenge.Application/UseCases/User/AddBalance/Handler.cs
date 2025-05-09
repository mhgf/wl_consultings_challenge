using WlChallenge.Application.UseCases.Abstractions;
using WlChallenge.Domain.Data.Abstractions;
using WlChallenge.Domain.Repositories;
using WlChallenge.Domain.Results;

namespace WlChallenge.Application.UseCases.User.AddBalance;

public class Handler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<Command, Response>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindById(request.UserId, cancellationToken);
        if (user is null)
            return Result.Failure<Response>("Usuário não encontrado.");

        user.AddAmountToWallet(request.Amount);

        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(new Response(user.Balance));
    }
}