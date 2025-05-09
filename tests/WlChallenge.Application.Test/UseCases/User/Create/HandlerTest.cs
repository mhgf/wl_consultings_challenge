using WlChallenge.Application.Test.Mocks;
using WlChallenge.Domain.Results;
using WlChallenge.Domain.ValueObjects;
using Command = WlChallenge.Application.UseCases.User.Create.Command;
using Handler = WlChallenge.Application.UseCases.User.Create.Handler;

namespace WlChallenge.Application.Test.UseCases.User.Create;

public class HandlerTest
{
    private readonly FakeUserRepository _fakeUserRepository = new();
    private readonly FakeUnitOfWork _fakeUnitOfWork = new();

    [Fact]
    public async Task ShouldFailIfEmailAlreadyExist()
    {
        var user = _fakeUserRepository.Users.First();

        var command = new Command(user.Name, user.Email, user.Document, FakeUserRepository.ValidPassword);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task ShouldFailIfDocumentAlreadyExist()
    {
        var user = _fakeUserRepository.Users.First();

        var command = new Command(user.Name, "teste@email.com", user.Document, FakeUserRepository.ValidPassword);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task ShouldCreateUserWithCnpj()
    {
        var user = _fakeUserRepository.Users.First();

        var command = new Command(user.Name, "teste_cnpj@email.com", "57286755000111", FakeUserRepository.ValidPassword);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        var newUser = _fakeUserRepository.Users.FirstOrDefault(x => x.Id == result.Value.Id);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Error.Message);
        Assert.NotNull(newUser);
        Assert.IsType<Cnpj>(newUser.Document);
    }

    [Fact]
    public async Task ShouldCreateUserWithCpf()
    {
        var user = _fakeUserRepository.Users.First();

        var command = new Command(user.Name, "teste_cpf@email.com", "490.401.720-05", FakeUserRepository.ValidPassword);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        var newUser = _fakeUserRepository.Users.FirstOrDefault(x => x.Id == result.Value.Id);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Error.Message);
        Assert.NotNull(newUser);
        Assert.IsType<Cpf>(newUser.Document);
    }
}