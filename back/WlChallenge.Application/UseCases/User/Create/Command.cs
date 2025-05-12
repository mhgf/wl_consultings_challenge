using WlChallenge.Application.UseCases.Abstractions;
using WlChallenge.Domain.Extensions;

namespace WlChallenge.Application.UseCases.User.Create;

public record Command : ICommand<Response>
{
    public Command(string name, string email, string document, string password)
    {
        Name = name;
        Email = email;
        Document = document.ToNumbers();
        Password = password;
    }

    public string Name { get; }
    public string Email { get; }
    public string Document { get; }
    public string Password { get; }
}