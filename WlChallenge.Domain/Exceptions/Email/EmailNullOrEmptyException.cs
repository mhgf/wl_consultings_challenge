namespace WlChallenge.Domain.Exceptions.Email;

public class EmailNullOrEmptyException(string message) : DomainException(message);
