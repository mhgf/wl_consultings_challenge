using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Enums;
using WlChallenge.Domain.Exceptions.Transaction;

namespace WlChallenge.Domain.Test.Entities;

public class TransactionTest
{
    [Theory]
    [InlineData(1, ETransactionType.Incoming)]
    [InlineData(100, ETransactionType.Outgoing)]
    [InlineData(10, ETransactionType.Incoming)]
    public void ShouldCreateTransaction(int amount, ETransactionType transactionType)
    {
        var senderId = Guid.NewGuid();
        var receiverId = Guid.NewGuid();

        var transaction = Transaction.Create(senderId, receiverId, amount, transactionType);
        Assert.NotNull(transaction);
        Assert.Equal(senderId, transaction.SenderId);
        Assert.Equal(receiverId, transaction.ReceiverId);
        Assert.Equal(transactionType, transaction.Type);
        Assert.NotEqual(Guid.Empty, transaction.Id);
    }

    [Theory]
    [InlineData(0, ETransactionType.Incoming)]
    [InlineData(-100, ETransactionType.Outgoing)]
    [InlineData(-10, ETransactionType.Incoming)]
    public void ShouldFailIfAmountIsInvalid(int amount, ETransactionType transactionType)
    {
        var senderId = Guid.NewGuid();
        var receiverId = Guid.NewGuid();

        Assert.Throws<InvalidTransactionAmountException>(() =>
            Transaction.Create(senderId, receiverId, amount, transactionType));
    }

    [Theory]
    [InlineData("0e2b0f02-b9c9-4ba4-9429-793611c748f7", "00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000000", "0e2b0f02-b9c9-4ba4-9429-793611c748f7")]
    public void ShouldFailIdSenderOrReceiverIsInvalid(Guid senderId, Guid receiverId)
    {
        Assert.Throws<InvalidTransactionUserIdException>(() =>
            Transaction.Create(senderId, receiverId, 1, ETransactionType.Incoming));
    }
}