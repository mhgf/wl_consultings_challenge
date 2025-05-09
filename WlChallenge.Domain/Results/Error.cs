namespace WlChallenge.Domain.Results;

public record Error(string Message)
{
    public static Error None = new(string.Empty);
    public static Error NullValue = new("Null.");

    #region Operators

    public static implicit operator Error(string error) => new(error);
    public static implicit operator string(Error error) => error.Message;

    #endregion
};