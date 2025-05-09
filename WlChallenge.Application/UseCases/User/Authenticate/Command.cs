using WlChallenge.Application.UseCases.Abstractions;

namespace WlChallenge.Application.UseCases.User.Authenticate;

public sealed record Command(string Email, string Password) : ICommand<Response>;