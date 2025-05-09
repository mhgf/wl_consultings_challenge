using WlChallenge.Domain.ValueObjects;

namespace WlChallenge.Domain.Test.ValueObjscts;

public class CnpjTest
{
    [Theory]
    public void ShouldCreateCnpj(string number)
    {
        var cnpj = Cnpj.Create(number);
        
    }
}