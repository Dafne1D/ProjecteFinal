using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class JwtService
{
    private readonly string _key = "super_s3cret_keY_very_1ong_03572989310";

    // GENERAR TOKEN
    public string GenerateToken(string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_key)
        );

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: "TaverEat",
            audience: "TaverEatUsers",
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // VALIDAR TOKEN (EMAIL)
    public string ValidateAndGetEmail(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        return jwt.Claims
            .First(x => x.Type == ClaimTypes.Email)
            .Value;
    }

    // SACAR ROLE
    public string GetRole(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        return jwt.Claims
            .First(x => x.Type == ClaimTypes.Role)
            .Value;
    }
}