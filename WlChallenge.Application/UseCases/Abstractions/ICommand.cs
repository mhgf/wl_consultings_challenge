using MediatR;
using WlChallenge.Domain.Results;

namespace WlChallenge.Application.UseCases.Abstractions;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseCommand where TResult : ICommandResponse;

public interface IBaseCommand;