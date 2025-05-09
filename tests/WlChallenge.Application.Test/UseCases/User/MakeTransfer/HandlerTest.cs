using WlChallenge.Application.Test.Mocks;
using WlChallenge.Domain.ValueObjects;
using Command = WlChallenge.Application.UseCases.User.MakeTransfer.Command;
using Handler = WlChallenge.Application.UseCases.User.MakeTransfer.Handler;

namespace WlChallenge.Application.Test.UseCases.User.MakeTransfer;

public class HandlerTest
{
    private readonly FakeUserRepository _fakeUserRepository = new();
    private readonly FakeUnitOfWork _fakeUnitOfWork = new();

    [Fact]
    public async Task ShouldFailIfNotFoundSender()
    {
        var command = new Command(Guid.NewGuid(), Guid.NewGuid(), 10);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task ShouldFailIfNotFoundReceiver()
    {
        var user = _fakeUserRepository.Users.First();

        var command = new Command(user.Id, Guid.NewGuid(), 10);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task ShouldFailIfDontHaveBalance()
    {
        var sender = _fakeUserRepository.Users.First();
        var receiver = _fakeUserRepository.Users.Last();

        var command = new Command(sender.Id, receiver.Id, 10);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Error.Message);
    }

    [Fact]
    public async Task ShouldMadeTheTransfer()
    {
        const int amountToTransfer = 110;
        const int initBalance = 1000;
        var sender = _fakeUserRepository.Users.First();
        var receiver = _fakeUserRepository.Users.Last();

        sender.AddAmountToWallet(initBalance);


        var command = new Command(sender.Id, receiver.Id, amountToTransfer);

        var handler = new Handler(_fakeUserRepository, _fakeUnitOfWork);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Error.Message);
        Assert.Equal((initBalance - amountToTransfer), sender.Balance);
        Assert.Equal(amountToTransfer, receiver.Balance);
        Assert.Equal(2, sender.Wallet.Transactions.Count);
        Assert.Single(receiver.Wallet.Transactions);
        Assert.Contains(sender.Wallet.Transactions, x => x.ReceiverId == receiver.Id && x.Amount == amountToTransfer);
        Assert.Contains(receiver.Wallet.Transactions, x => x.SenderId == sender.Id && x.Amount == amountToTransfer);
        ;
    }
}