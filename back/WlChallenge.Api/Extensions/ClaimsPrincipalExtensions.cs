using System.Security.Claims;

namespace WlChallenge.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Sid)?
            .Value;
        
        return Guid.TryParse(userId, out var guid) ? guid : Guid.Empty;
    }
}