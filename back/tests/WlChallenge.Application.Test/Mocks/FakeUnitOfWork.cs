using WlChallenge.Domain.Data.Abstractions;

namespace WlChallenge.Application.Test.Mocks;

public class FakeUnitOfWork : IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken = default)
        => Task.Delay(1, cancellationToken);
}