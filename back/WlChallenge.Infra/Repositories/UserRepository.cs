using Microsoft.EntityFrameworkCore;
using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Repositories;
using WlChallenge.Infra.Data;

namespace WlChallenge.Infra.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    private readonly DbSet<User> _users = dbContext.Set<User>();

    public async Task<User?> FindById(Guid id, CancellationToken cancellationToken = default)
        => await _users
            .Include(x => x.Wallet)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await _users.FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);

    public Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default)
        => _users.AnyAsync(x => x.Email.Address == email, cancellationToken);

    public async Task<bool> ExistDocumentAsync(string document, CancellationToken cancellationToken = default)
        => await _users.AnyAsync(x => x.Document.Number == document, cancellationToken);

    public async Task SaveAsync(User account, CancellationToken cancellationToken = default)
        => await _users.AddAsync(account, cancellationToken);

    public void Update(User account, CancellationToken cancellationToken = default)
        => _users.Update(account);
}