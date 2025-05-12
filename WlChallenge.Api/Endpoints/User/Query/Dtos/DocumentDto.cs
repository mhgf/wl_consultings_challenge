using WlChallenge.Domain.Enums;

namespace WlChallenge.Api.Endpoints.User.Query.Dtos;

public record DocumentDto
{
    public string Number { get; }
    public EDocumentType Type { get; }

    public DocumentDto(string number, EDocumentType type)
    {
        Number = number.Trim();
        Type = type;
    }
}