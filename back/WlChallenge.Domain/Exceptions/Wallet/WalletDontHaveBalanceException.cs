namespace WlChallenge.Domain.Exceptions.Wallet;

public class WalletDontHaveBalanceException(string message) : DomainException(message);