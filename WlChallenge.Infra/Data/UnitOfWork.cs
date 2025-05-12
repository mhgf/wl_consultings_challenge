using WlChallenge.Domain.Data.Abstractions;

namespace WlChallenge.Infra.Data;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
        => await dbContext.SaveChangesAsync(cancellationToken);
}