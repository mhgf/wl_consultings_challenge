namespace WlChallenge.Api.Endpoints.User.Command.Dtos;

public record MakeTransferDto(Guid Receiver, int Amount);