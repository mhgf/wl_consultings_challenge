using WlChallenge.Domain.Enums;
using WlChallenge.Domain.Errors;
using WlChallenge.Domain.Exceptions.Wallet;

namespace WlChallenge.Domain.Entities;

public class Wallet : Entity
{
    #region Contructors

    private Wallet()
    {
    }

    private Wallet(User user)
        => UserId = user.Id;

    private Wallet(Guid userId)
        => UserId = userId;

    #endregion

    #region Factory

    public static Wallet Create(User user) => new(user);
    public static Wallet Create(Guid userId) => new(userId);

    #endregion

    #region Properties

    public Guid UserId { get; private set; }
    public int Balance { get; private set; }

    public User User { get; private set; } = null!;

    private List<Transaction> _transactions = [];
    public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

    #endregion

    #region Public Methods

    public void AddAmount(int amount)
    {
        if (amount <= 0) return;
        Balance += amount;
        _transactions.Add(Transaction.Create(UserId, UserId, amount, ETransactionType.Incoming));
    }

    public void ReceiveAmount(Guid senderId, int amount)
    {
        if (senderId == Guid.Empty)
            throw new InvalidSenderUserException(ErrorMessages.Wallet.InvalidSenderId);

        if (amount <= 0) return;
        Balance += amount;
        _transactions.Add(Transaction.Create(senderId, UserId, amount, ETransactionType.Incoming));
    }

    public void SendAmount(Guid receiverId, int amount)
    {
        if (receiverId == Guid.Empty)
            throw new InvalidReceiverUserException(ErrorMessages.Wallet.InvalidReceiverId);
        
        
        if (amount <= 0) return;
        var balance = Balance - amount;
        if (balance < 0)
            throw new WalletDontHaveBalanceException(ErrorMessages.Wallet.NoBalance);

        Balance = balance;
        _transactions.Add(Transaction.Create(UserId, receiverId, amount, ETransactionType.Outgoing));
    }
    
    

    #endregion
}