namespace WlChallenge.Domain.Exceptions.Document;

public class InvalidCpfNumberException(string message) : DomainException(message);