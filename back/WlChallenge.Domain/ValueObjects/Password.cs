using System.Security.Cryptography;
using WlChallenge.Domain.Errors;
using WlChallenge.Domain.Exceptions.Password;

namespace WlChallenge.Domain.ValueObjects;

public sealed record Password
{
    #region Constants

    public const short MinimumLength = 12;
    public const short MaximumLength = 48;

    #endregion

    #region Constructors

    private Password()
    {
    }

    private Password(string plainTextPassword) => HashText = Hash(plainTextPassword);

    #endregion

    #region Factories

    public static Password Create(string plainTextPassword)
        => new(plainTextPassword);

    #endregion

    #region Operators

    public static implicit operator string(Password password) => password.HashText;

    #endregion

    #region Properties

    public string HashText { get; private set; } = string.Empty;

    #endregion

    #region Private Methods

    private static string Hash(string password)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            throw new PasswordNullException(ErrorMessages.Password.Invalid);

        if (password.Length < MinimumLength || password.Length > MaximumLength)
            throw new PasswordLengthException(ErrorMessages.Password.InvalidLength);

        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    #endregion

    #region Public Methods

    public bool Verify(string plainTextPassword)
        => BCrypt.Net.BCrypt.Verify(plainTextPassword, HashText);

    #endregion
}