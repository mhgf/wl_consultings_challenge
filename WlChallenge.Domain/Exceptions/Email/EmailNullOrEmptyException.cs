namespace WlChallenge.Domain.Exceptions;

public class EmailNullOrEmptyException(string message) : DomainException(message);
