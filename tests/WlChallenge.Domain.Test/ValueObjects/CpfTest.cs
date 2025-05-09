using WlChallenge.Domain.Enums;
using WlChallenge.Domain.Exceptions.Document;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.ValueObjects;

public class CnpjTest
{
    [Theory]
    [InlineData("57286755000111")]
    [InlineData("55825965000105")]
    [InlineData("52109719000169")]
    [InlineData("47296148000189")]
    [InlineData("47fd296148000189 ")]
    public void ShouldCreateCnpj(string number)
    {
        var cnpj = Cnpj.Create(number);
        Assert.NotNull(cnpj);
        Assert.Equal(EDocumentType.Cnpj, cnpj.Type);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldFailIfIsNullOrEmptyOrWithSpace(string number)
    {
        Assert.Throws<CnpjNullOrEmptyException>(() => Cnpj.Create(number));
    }
    
    [Theory]
    [InlineData("57286755000142424411")]
    [InlineData("4234")]
    public void ShouldFailIfNumberIsInvalidLenght(string number)
    {
        Assert.Throws<InvalidCnpjLenghtException>(() => Cnpj.Create(number));
    }
    
    [Theory]
    [InlineData("87286755000111")]
    [InlineData("75825965000105")]
    [InlineData("62109719000169")]
    [InlineData("57296148000189")]
    [InlineData("47fd296148002189 ")]
    public void ShouldFailIfNumberIsInvalid(string number)
    {
        Assert.Throws<InvalidCnpjException>(() => Cnpj.Create(number));
    }
}