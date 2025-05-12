using WlChallenge.Domain.Entities;
using WlChallenge.Domain.Exceptions.User;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.Entities;

public class UserTest
{
    private readonly Email _email = Email.Create("user@teste.com");
    private readonly Password _password = Password.Create("12345612312312327317@3");
    private readonly Document _document = Cpf.Create("34784810072");

    [Theory]
    [InlineData("ana")]
    [InlineData("teste teste teste")]
    [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean m")]
    public void ShouldCreateUser(string name)
    {
        var user = User.Create(name, _email, _password, _document);
        Assert.NotNull(user);
        Assert.Equal(name, user.Name);
        Assert.Equal(_email, user.Email);
        Assert.Equal(_password, user.Password);
        Assert.Equal(_document, user.Document);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    public void ShouldFailIfNameIsNullOrEmpty(string name)
    {
        Assert.Throws<UserNameNullException>(() => User.Create(name, _email, _password, _document));
    }

    [Theory]
    [InlineData("q")]
    [InlineData("qr")]
    [InlineData(
        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean ma")]
    public void ShouldFailIfNameLenghtIsInvalid(string name)
    {
        Assert.Throws<InvalidNameLengthException>(() => User.Create(name, _email, _password, _document));
    }
}