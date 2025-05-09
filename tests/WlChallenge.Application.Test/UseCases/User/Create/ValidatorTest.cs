using WlChallenge.Application.Test.Mocks;
using Command = WlChallenge.Application.UseCases.User.Create.Command;
using Validator = WlChallenge.Application.UseCases.User.Create.Validator;

namespace WlChallenge.Application.Test.UseCases.User.Create;

public class ValidatorTest
{
    private const string ValidEmail = "teste_email@teste.com";
    private const string ValidCnpj = "57286755000111";
    private const string ValidCpf = "490.401.720-05";
    private const string ValidPassword = FakeUserRepository.ValidPassword;


    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("a")]
    [InlineData("ad")]
    [InlineData(
        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean md")]
    public void ShouldFailIfNameIsIsInvalid(string name)
    {
        var validator = new Validator();

        var command = new Command(name, ValidEmail, ValidCpf, ValidPassword);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Name), error.PropertyName));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("a@")]
    [InlineData("@c.ocm")]
    public void ShouldFailIfEmailIsIsInvalid(string email)
    {
        var validator = new Validator();

        var command = new Command("Ana", email, ValidCpf, ValidPassword);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Email), error.PropertyName));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("1234567")]
    [InlineData("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean m")]
    public void ShouldFailIfDocumentLenghtIsIsInvalid(string document)
    {
        var validator = new Validator();

        var command = new Command("Ana", ValidEmail, document, ValidPassword);
        var result = validator.Validate(command);


        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Document), error.PropertyName));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("!fsdfaf")]
    [InlineData("e$deA26#kKYuXdcw8r9c940r8wffEaOJ*g1&f8wtK2J3fDR*@")]
    public void ShouldFailIfPasswordIsInvalid(string password)
    {
        var validator = new Validator();

        var command = new Command("ana", ValidEmail, ValidCpf, password);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Password), error.PropertyName));
    }

    [Fact]
    public void ShouldPass()
    {
        var validator = new Validator();

        var result = validator.Validate(new Command("Ana", ValidEmail, ValidCnpj, ValidPassword));

        Assert.True(result.IsValid);
    }
}