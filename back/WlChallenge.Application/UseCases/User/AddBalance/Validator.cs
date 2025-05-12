using FluentValidation;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Application.UseCases.User.AddBalance;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty).WithMessage("O id do usuário é obrigatório.");

        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
    }
}