using WlChallenge.Application.Test.Mocks;
using Command = WlChallenge.Application.UseCases.User.Authenticate.Command;
using Validator = WlChallenge.Application.UseCases.User.Authenticate.Validator;

namespace WlChallenge.Application.Test.UseCases.User.Authenticate;

public class ValidatorTest
{
    private const string ValidEmail = "teste_email@teste.com";
    private const string ValidPassword = FakeUserRepository.ValidPassword;

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("tsete#teste.com")]
    [InlineData("tsete()teste.com")]
    public void ShouldFailIfEmailIsIsInvalid(string email)
    {
        var validator = new Validator();

        var command = new Command(email, ValidPassword);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Email), error.PropertyName));
    }

    [Theory]
    [InlineData("!fsdfaf")]
    [InlineData("e$deA26#kKYuXdcw8r9c940r8wffEaOJ*g1&f8wtK2J3fDR*@")]
    public void ShouldFailIfPasswordIsInvalid(string password)
    {
        var validator = new Validator();

        var command = new Command(ValidEmail, password);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Password), error.PropertyName));
    }

    [Fact]
    public void ShouldPass()
    {
        var validator = new Validator();

        var result = validator.Validate(new Command(ValidEmail, ValidPassword));

        Assert.True(result.IsValid);
    }
}