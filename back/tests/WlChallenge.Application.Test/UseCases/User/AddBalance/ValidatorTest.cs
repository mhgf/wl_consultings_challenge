using Command = WlChallenge.Application.UseCases.User.AddBalance.Command;
using Validator = WlChallenge.Application.UseCases.User.AddBalance.Validator;

namespace WlChallenge.Application.Test.UseCases.User.AddBalance;

public class ValidatorTest
{
    [Fact]
    public void ShouldFailIfIdIsIsInvalid()
    {
        var validator = new Validator();

        var command = new Command(Guid.Empty, 10);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.UserId), error.PropertyName));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldFailIfAmountIsInvalid(int amount)
    {
        var validator = new Validator();

        var command = new Command(Guid.NewGuid(), amount);
        var result = validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Amount), error.PropertyName));
    }

    [Fact]
    public void ShouldPass()
    {
        var validator = new Validator();

        var result = validator.Validate(new Command(Guid.NewGuid(), 10));

        Assert.True(result.IsValid);
    }
}