using WlChallenge.Domain.Enums;
using WlChallenge.Domain.Exceptions.Document;
using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.ValueObjects;

public class CpfTest
{
    [Theory]
    [InlineData("110101@84024")]
    [InlineData("58269^979074")]
    [InlineData("233105190gfd90")]
    [InlineData("34784810072")]
    [InlineData("635 29249050")]
    public void ShouldCreateCpf(string number)
    {
        var cpf = Cpf.Create(number);
        Assert.NotNull(cpf);
        Assert.Equal(EDocumentType.Cpf, cpf.Type);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldFailIfIsNullOrEmptyOrWithSpace(string number)
    {
        Assert.Throws<CpfNullOrEmptyException>(() => Cpf.Create(number));
    }

    [Theory]
    [InlineData("57286755000142424411")]
    [InlineData("4234")]
    public void ShouldFailIfNumberIsInvalidLenght(string number)
    {
        Assert.Throws<InvalidCpfLenghtException>(() => Cpf.Create(number));
    }

    [Theory]
    [InlineData("1101fd0124024")]
    [InlineData("52269ds979074")]
    [InlineData("23314519090")]
    [InlineData("34784810074")]
    [InlineData("63529219 050")]
    public void ShouldFailIfNumberIsInvalid(string number)
    {
        Assert.Throws<InvalidCpfException>(() => Cpf.Create(number));
    }
}