using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Exceptions.Wallet;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.Entities;

public class WalletTest
{
    [Fact]
    public void ShouldCreateWallet()
    {
        var wallet = Wallet.Create(Guid.NewGuid());
        Assert.NotNull(wallet);
        Assert.Equal(0, wallet.Balance);
        Assert.NotEqual(Guid.Empty, wallet.Id);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1040)]
    public void ShouldAddIntoBalance(int amount)
    {
        var wallet = Wallet.Create(Guid.NewGuid());

        wallet.AddAmount(amount);

        Assert.NotNull(wallet);
        Assert.Equal(amount, wallet.Balance);
        Assert.Single(wallet.Transactions);
    }

    [Fact]
    public void ShouldFailIfSenderIsInvalid()
    {
        var wallet = Wallet.Create(Guid.NewGuid());
        Assert.Throws<InvalidSenderUserException>(() => wallet.ReceiveAmount(Guid.Empty, 1));
    }

    [Fact]
    public void ShouldFailIfReceiverIsInvalid()
    {
        var wallet = Wallet.Create(Guid.NewGuid());
        Assert.Throws<InvalidReceiverUserException>(() => wallet.SendAmount(Guid.Empty, 1));
    }

    [Fact]
    public void ShouldFailIfDontHaveBalance()
    {
        var wallet = Wallet.Create(Guid.NewGuid());
        Assert.Throws<WalletDontHaveBalanceException>(() => wallet.SendAmount(Guid.NewGuid(), 1));
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1040)]
    public void ShouldReceiveAmountFromSender(int amount)
    {
        var wallet = Wallet.Create(Guid.NewGuid());
        wallet.ReceiveAmount(Guid.NewGuid(), amount);


        Assert.Equal(amount, wallet.Balance);
        Assert.Single(wallet.Transactions);
    }

    [Theory]
    [InlineData(100, 10)]
    [InlineData(10000, 4000)]
    [InlineData(20, 10)]
    public void ShouldSendToAnotherUser(int add, int send)
    {
        var wallet = Wallet.Create(Guid.NewGuid());
        wallet.AddAmount(add);
        wallet.SendAmount(Guid.NewGuid(), send);

        Assert.Equal((add - send), wallet.Balance);
        Assert.Equal(2, wallet.Transactions.Count);
    }
}