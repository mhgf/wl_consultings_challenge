using System.Text.RegularExpressions;
using WlChallenge.Domain.Errors;
using WlChallenge.Domain.Exceptions;
using WlChallenge.Domain.Exceptions.Email;

namespace WlChallenge.Domain.ValueObjects;

public sealed partial record Email
{
    #region Constants

    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    #endregion

    #region Constructor

    private Email()
    {
        Address = string.Empty;
    }

    private Email(string address)
    {
        Address = address;
    }

    #endregion

    #region Factories

    public static Email Create(string address)
    {
        if (string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(address))
            throw new EmailNullOrEmptyException(ErrorMessages.Email.NullOrEmpty);
        address = address.Trim().ToLower();

        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException(ErrorMessages.Email.Invalid);

        return new Email(address);
    }

    #endregion


    #region Operators

    public static implicit operator string(Email email) => email.ToString();

    #endregion

    #region Overrides

    public override string ToString() => Address;

    #endregion

    #region Other

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    #endregion

    #region Properties

    public string Address { get; private set; }

    #endregion
};
