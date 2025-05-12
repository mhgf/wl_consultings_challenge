namespace WlChallenge.Api.Endpoints.User.Query.Dtos;

public record UserDto(Guid Id, string Name, string Email, DocumentDto Document);