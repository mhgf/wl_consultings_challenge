using WlChallenge.Application.UseCases.Abstractions;
using WlChallenge.Domain.Repositories;
using WlChallenge.Domain.Results;

namespace WlChallenge.Application.UseCases.User.Authenticate;

public class Handler(IUserRepository userRepository) : ICommandHandler<Command, Response>
{
    private const string DefaultMessage = "E-mail ou sem inválidos";

    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(request.Email, cancellationToken);
        if (user is null || !user.Password.Verify(request.Password))
            return Result.Failure<Response>(DefaultMessage);

        return Result.Ok(new Response(user.Id, user.Name, user.Email));
    }
}