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

    private Transaction(Guid walletId, Guid senderId, Guid receiverId, int amount, ETransactionType type)
    {
        WalletId = walletId;
        SenderId = senderId;
        ReceiverId = receiverId;
        Amount = (uint)amount;
        Type = type;
    }

    #endregion

    #region Factories

    public static Transaction Create(Guid walletId, Guid senderId, Guid receiverId, int amount, ETransactionType type)
    {
        if (amount <= 0)
            throw new InvalidTransactionAmountException(ErrorMessages.Transaction.InvalidAmount);
        if (walletId == Guid.Empty)
            throw new InvalidTransactionUserIdException(ErrorMessages.Transaction.InvalidSenderId);
        if (senderId == Guid.Empty)
            throw new InvalidTransactionUserIdException(ErrorMessages.Transaction.InvalidSenderId);
        if (receiverId == Guid.Empty)
            throw new InvalidTransactionUserIdException(ErrorMessages.Transaction.InvalidReceiverId);

        return new Transaction(walletId, senderId, receiverId, amount, type);
    }

    #endregion

    #region Properties

    public Guid WalletId { get; private set; }
    public Guid SenderId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public uint Amount { get; private set; }
    public ETransactionType Type { get; private set; }

    public Wallet Wallet { get; private set; } = null!;
    public User Sender { get; private set; } = null!;
    public User Receiver { get; private set; } = null!;

    #endregion
}