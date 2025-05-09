using FluentValidation;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Application.UseCases.User.Create;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
        RuleFor(x => x.Name).MinimumLength(Domain.Entities.User.MinNameLength)
            .WithMessage($"O nome deve conter pelo menos {Domain.Entities.User.MinNameLength} caracteres.");
        RuleFor(x => x.Name).MaximumLength(Domain.Entities.User.MaxNameLength)
            .WithMessage($"O nome deve conter no máximo {Domain.Entities.User.MaxNameLength} caracteres.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("O E-mail é obrigatório.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("O E-mail informado é inválido.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");
        RuleFor(x => x.Password).MinimumLength(Password.MinimumLength)
            .WithMessage($"A senha deve conter pelo menos {Password.MinimumLength} caracteres.");
        RuleFor(x => x.Password).MaximumLength(Password.MaximumLength)
            .WithMessage($"A senha deve conter no máximo {Password.MaximumLength} caracteres.");

        RuleFor(x => x.Document).NotEmpty().WithMessage("O Documento é obrigatório.");
        RuleFor(x => x.Document)
            .Must(x => x.Length is Cpf.MinLength or Cnpj.MinLength)
            .WithMessage("Documento");
    }
}