using WlChallenge.Domain.Entities;

namespace WlChallenge.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task SaveAsync(User account, CancellationToken cancellationToken = default);
    void UpdateAsync(User account, CancellationToken cancellationToken = default);
}