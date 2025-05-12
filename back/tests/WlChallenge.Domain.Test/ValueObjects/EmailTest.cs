using WlChallenge.Domain.Exceptions.Email;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.ValueObjects;

public class EmailTest
{
    [Theory]
    [InlineData("lara-goncalves70@ladder.com.br")]
    [InlineData("juliojosenogueira@onvale.com")]
    [InlineData("aurora_luiza_dacosta@platinium.com.br")]
    public void ShouldCreateAnEmail(string address)
    {
        var email = Email.Create(address);
        Assert.NotNull(email);
        Assert.Equal(address, email.Address);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void ShouldFailIfEmailIsEmptyOrWithSpace(string address)
    {
        Assert.Throws<EmailNullOrEmptyException>(() => Email.Create(address));
    }
    
    [Theory]
    [InlineData("dsd")]
    [InlineData("lara-goncalves70#ladder.com.br")]
    [InlineData("juliojosenogueira@@onvale.com")]
    public void ShouldFailIfEmailIsInvalid(string address)
    {
        Assert.Throws<InvalidEmailException>(() => Email.Create(address));
    }
}