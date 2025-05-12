using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Repositories;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Application.Test.Mocks;

public class FakeUserRepository : IUserRepository
{
    public const string ValidPassword = "12345612312312327317@3";

    private List<User> _users = [];
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    public FakeUserRepository(ushort totalUsers = 20)
    {
        var password = Password.Create(ValidPassword);
        var document = Cpf.Create("34784810072");

        for (var i = 0; i < totalUsers; i++)
            _users.Add(User.Create($"User Teste {i}",
                Email.Create($"user{i}@teste.com"),
                password, document));
    }

    public Task<User?> FindById(Guid id, CancellationToken cancellationToken = default)
        => Task.FromResult<User?>(_users.SingleOrDefault(x => x.Id == id));

    public Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        => Task.FromResult<User?>(_users.SingleOrDefault(x => x.Email.Address == email));

    public Task<bool> ExistEmailAsync(string email, CancellationToken cancellationToken = default)
        => Task.FromResult(_users.Exists(x => x.Email.Address == email));

    public Task<bool> ExistDocumentAsync(string document, CancellationToken cancellationToken = default)
        => Task.FromResult(_users.Exists(x => x.Document.Number == document));

    public Task SaveAsync(User account, CancellationToken cancellationToken = default)
    {
        _users.Add(account);
        return Task.CompletedTask;
    }

    public void Update(User account, CancellationToken cancellationToken = default)
    {
        return;
    }
}