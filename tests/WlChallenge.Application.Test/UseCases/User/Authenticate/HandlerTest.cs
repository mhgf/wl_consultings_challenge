using WlChallenge.Application.Test.Mocks;
using WlChallenge.Domain.Results;
using Command = WlChallenge.Application.UseCases.User.Authenticate.Command;
using Handler = WlChallenge.Application.UseCases.User.Authenticate.Handler;

namespace WlChallenge.Application.Test.UseCases.User.Authenticate;

public class HandlerTest
{
    private readonly FakeUserRepository _fakeUserRepository = new();

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("tsete#teste.com")]
    [InlineData("tsete()teste.com")]
    public async Task ShouldFailIfNotFindTheUser(string email)
    {
        var command = new Command(email, FakeUserRepository.ValidPassword);

        var handler = new Handler(_fakeUserRepository);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("tsete#teste.com")]
    [InlineData("tsete()teste.com")]
    public async Task ShouldFailIfPasswordIsIncorrect(string password)
    {
        var email = _fakeUserRepository.Users.First().Email.Address;

        var command = new Command(email, password);

        var handler = new Handler(_fakeUserRepository);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task ShouldValidateUser()
    {
        var user = _fakeUserRepository.Users.First();
        var email = user.Email.Address;

        var command = new Command(email, FakeUserRepository.ValidPassword);

        var handler = new Handler(_fakeUserRepository);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(Error.None, result.Error.Message);
        Assert.Equal(user.Id, result.Value.UserId);
        Assert.Equal(user.Name, result.Value.UserName);
        Assert.Equal(user.Email.Address, result.Value.Email);
    }
}