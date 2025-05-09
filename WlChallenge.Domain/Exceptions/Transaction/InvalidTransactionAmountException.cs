namespace WlChallenge.Domain.Exceptions.Transaction;

public class InvalidTransactionAmountException(string message) : DomainException(message);