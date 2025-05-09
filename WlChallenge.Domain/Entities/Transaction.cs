using WlChallenge.Domain.Enums;
using WlChallenge.Domain.Errors;
using WlChallenge.Domain.Exceptions.Transaction;

namespace WlChallenge.Domain.Entities;

public class Transaction : Entity
{
    #region Constructors

    private Transaction()
    {
    }

    private Transaction(Guid senderId, Guid receiverId, int amount, ETransactionType type)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Amount = (uint)amount;
        Type = type;
    }

    #endregion

    #region Factories

    public static Transaction Create(Guid senderId, Guid receiverId, int amount, ETransactionType type)
    {
        if (amount <= 0)
            throw new InvalidTransactionAmountException(ErrorMessages.Transaction.InvalidAmount);
        if (senderId == Guid.Empty)
            throw new InvalidTransactionUserIdException(ErrorMessages.Transaction.InvalidSenderId);
        if (receiverId == Guid.Empty)
            throw new InvalidTransactionUserIdException(ErrorMessages.Transaction.InvalidReceiverId);

        return new Transaction(senderId, receiverId, amount, type);
    }

    #endregion

    #region Properties

    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public uint Amount { get; private set; }
    public ETransactionType Type { get; set; }

    public User Sender { get; private set; } = null!;
    public User Receiver { get; private set; } = null!;

    #endregion
}