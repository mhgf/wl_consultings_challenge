namespace WlChallenge.Domain.Extensions;

public static class StringExtension
{
    #region Public Methods

    public static string ToNumbers(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : new string(value.Where(char.IsDigit).ToArray());
    }

    #endregion
}