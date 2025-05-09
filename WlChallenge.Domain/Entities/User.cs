using WlChallenge.Domain.Entities.Abstractions;
using WlChallenge.Domain.Errors;
using WlChallenge.Domain.Exceptions.User;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    #region Constants

    public const ushort MinNameLength = 3;
    public const ushort MaxNameLength = 100;

    #endregion

    #region Constructors

    private User()
    {
    }

    private User(
        string name,
        Email email,
        Password password,
        Document document)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            throw new UserNameNullException(ErrorMessages.User.InvalidNullOrEmpty);
        name = name.Trim();
        if (name.Length is < MinNameLength or > MaxNameLength)
            throw new InvalidNameLengthException(ErrorMessages.User.InvalidLength);

        Name = name;
        Email = email;
        Password = password;
        Document = document;
        Wallet = Wallet.Create(this);
    }

    #endregion

    #region Factories

    public static User Create(string name, Email email, Password password,
        Document document) => new(name, email, password, document);

    #endregion

    #region Properties

    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public Document Document { get; private set; } = null!;

    public Wallet Wallet { get; private set; } = null!;

    #endregion
}