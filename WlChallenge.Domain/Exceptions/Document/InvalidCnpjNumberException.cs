namespace WlChallenge.Domain.Exceptions.Document;

public class InvalidCnpjNumberException(string message) : Exception(message);