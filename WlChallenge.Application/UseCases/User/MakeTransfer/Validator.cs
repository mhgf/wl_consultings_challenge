using FluentValidation;

namespace WlChallenge.Application.UseCases.User.MakeTransfer;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.SenderId).NotEqual(Guid.Empty).WithMessage("O id do rementente é obrigatório.");
        RuleFor(x => x.ReceiverId).NotEqual(Guid.Empty).WithMessage("O id do destinatario é obrigatório.");

        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
    }
}