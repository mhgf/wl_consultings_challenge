using WlChallenge.Application.Test.Mocks;
using Command = WlChallenge.Application.UseCases.User.MakeTransfer.Command;
using Validator = WlChallenge.Application.UseCases.User.MakeTransfer.Validator;

namespace WlChallenge.Application.Test.UseCases.User.MakeTransfer;

public class ValidatorTest
{
    [Fact]
    private void ShouldFailIfSenderIsInvalid()
    {
        var command = new Command(Guid.Empty, Guid.NewGuid(), 10);
        var result = new Validator().Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.SenderId), error.PropertyName));
    }

    [Fact]
    private void ShouldFailIfReceiverIsInvalid()
    {
        var command = new Command(Guid.NewGuid(), Guid.Empty, 10);
        var result = new Validator().Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.ReceiverId), error.PropertyName));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-300)]
    private void ShouldFailIfAmountIsInvalid(int amount)
    {
        var command = new Command(Guid.NewGuid(), Guid.NewGuid(), amount);
        var result = new Validator().Validate(command);

        Assert.False(result.IsValid);
        Assert.All(result.Errors, error => Assert.Equal(nameof(command.Amount), error.PropertyName));
    }

    [Fact]
    private void ShouldPass()
    {
        var command = new Command(Guid.NewGuid(), Guid.NewGuid(), 300);
        var result = new Validator().Validate(command);

        Assert.True(result.IsValid);
    }
}