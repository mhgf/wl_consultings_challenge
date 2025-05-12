using FluentValidation;
using MediatR;
using WlChallenge.Application.Exceptions;
using WlChallenge.Application.UseCases.Abstractions;
using ValidationException = WlChallenge.Application.Exceptions.ValidationException;

namespace WlChallenge.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validators.Any() == false)
            return await next(cancellationToken);

        var context = new ValidationContext<TRequest>(request);
        var validationErrors = validators
            .Select(x => x.Validate(context))
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .Select(x => new ValidationError(x.PropertyName, x.ErrorMessage))
            .ToList();

        if (validationErrors.Count != 0)
            throw new ValidationException(validationErrors);

        return await next(cancellationToken);
    }
}