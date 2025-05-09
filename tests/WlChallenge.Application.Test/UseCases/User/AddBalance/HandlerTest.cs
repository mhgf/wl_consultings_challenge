using WlChallenge.Application.Test.Mocks;
using Command = WlChallenge.Application.UseCases.User.AddBalance.Command;
using Handler = WlChallenge.Application.UseCases.User.AddBalance.Handler;

namespace WlChallenge.Application.Test.UseCases.User.AddBalance;

public class HandlerTest
{
    private readonly FakeUserRepository _fakeUserRepository = new();
    private readonly FakeUnitOfWork _fakeUnitOfWork = new();

    [Fact]
    public async Task ShouldFailWithInvalidUserId()
    {
        var command = new Command(Guid.NewGuid(), 10);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(5050)]
    [InlineData(99999)]
    public async Task ShouldAddBalanceToWallet(int amount)
    {
        var userId = _fakeUserRepository.Users.First().Id;
        var command = new Command(userId, amount);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(amount, result.Value.Balance);
    }
}