using WlChallenge.Domain.Entities;

namespace WlChallenge.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> FindById(Guid id, CancellationToken cancellationToken = default);
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistDocumentAsync(string document, CancellationToken cancellationToken = default);
    Task SaveAsync(User account, CancellationToken cancellationToken = default);
    void Update(User account, CancellationToken cancellationToken = default);
}