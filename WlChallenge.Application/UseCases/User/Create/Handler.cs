using WlChallenge.Application.UseCases.Abstractions;
using WlChallenge.Domain.Data.Abstractions;
using WlChallenge.Domain.Repositories;
using WlChallenge.Domain.Results;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Application.UseCases.User.Create;

public class Handler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<Command, Response>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistEmailAsync(request.Email, cancellationToken))
            return Result.Failure<Response>("Email já existe.");

        if (await userRepository.ExistDocumentAsync(request.Document, cancellationToken))
            return Result.Failure<Response>("Documento já existe.");

        var email = Email.Create(request.Email);
        Document document = request.Document.Length == Cpf.MinLength
            ? Cpf.Create(request.Document)
            : Cnpj.Create(request.Document);
        var password = Password.Create(request.Password);

        var user = Domain.Entities.User.Create(request.Name, email, password, document);

        await userRepository.SaveAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok(new Response(user.Id));
    }
}