namespace WlChallenge.Domain.Exceptions.Transaction;

public class InvalidTransactionUserIdException(string message) : DomainException(message);