using WlChallenge.Domain.Exceptions.Password;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.ValueObjects;

public class PasswordTest
{
    [Theory]
    [InlineData("!$fH33mLYITnUkYt")]
    [InlineData("!z6Wjud6qJ%VwEDWfo@e5b!BURF*J&")]
    public void ShouldCreatePassword(string value)
    {
        var password = Password.Create(value);

        Assert.NotNull(password);
        Assert.NotEqual(value, password.HashText);
    }

    [Theory]
    [InlineData("!fsdfaf")]
    [InlineData("e$deA26#kKYuXdcw8r9c940r8wffEaOJ*g1&f8wtK2J3fDR*@")]
    public void ShouldFailIfLengthIsNotCorrect(string value)
    {
        Assert.Throws<PasswordLengthException>(() => Password.Create(value));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    public void ShouldFailIfIsEmptyOrWithSpace(string value)
    {
        Assert.Throws<PasswordNullException>(() => Password.Create(value));
    }
}