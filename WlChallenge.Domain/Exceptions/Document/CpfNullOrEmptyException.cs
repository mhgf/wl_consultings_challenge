namespace WlChallenge.Domain.Exceptions.Document;

public class CpfNullOrEmptyException(string message) : Exception(message);