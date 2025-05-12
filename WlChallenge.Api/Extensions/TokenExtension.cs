using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WlChallenge.Api.Shared;
using ResponseType = WlChallenge.Application.UseCases.User.Authenticate.Response;

namespace WlChallenge.Api.Extensions;

public static class TokenExtension
{
    public static string GenerateToken(this ResponseType response)
    {
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, response.UserName),
                    new Claim(ClaimTypes.Email, response.Email),
                    new Claim(ClaimTypes.Sid, response.UserId.ToString())
                ]),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}