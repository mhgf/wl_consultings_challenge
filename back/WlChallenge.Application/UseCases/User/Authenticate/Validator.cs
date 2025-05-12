using FluentValidation;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Application.UseCases.User.Authenticate;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("O E-mail é obrigatório.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("O E-mail informado é inválido.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");
        RuleFor(x => x.Password).MinimumLength(Password.MinimumLength)
            .WithMessage($"A senha deve conter pelo menos {Password.MinimumLength} caracteres.");
        RuleFor(x => x.Password).MaximumLength(Password.MaximumLength)
            .WithMessage($"A senha deve conter no máximo {Password.MaximumLength} caracteres.");
    }
}