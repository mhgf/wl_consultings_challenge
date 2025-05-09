namespace WlChallenge.Domain.Exceptions.Wallet;

public class WalletDontHaveBalanceExeption(string message) : DomainException(message);