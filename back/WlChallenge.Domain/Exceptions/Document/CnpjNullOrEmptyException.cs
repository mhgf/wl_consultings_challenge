namespace WlChallenge.Domain.Exceptions.Document;

public class CnpjNullOrEmptyException(string message) : Exception(message);