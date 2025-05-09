namespace WlChallenge.Domain.Exceptions.Document;

public class InvalidCnpjException(string message) : DomainException(message);