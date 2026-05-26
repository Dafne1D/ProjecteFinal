using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class JwtService
{
    private readonly string _key = "TaverEat_super_secure_key_2026_very_long_1924871";

    // GENERAR TOKEN
    public string GenerateToken(string email, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // VALIDAR TOKEN
    public ClaimsPrincipal Validate(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key);

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        return handler.ValidateToken(token, parameters, out _);
    }

    // EXTRAER EMAIL
    public string ValidateAndGetEmail(string token)
    {
        var principal = Validate(token);
        return principal.FindFirst(ClaimTypes.Email)?.Value;
    }

    // EXTRAER ROLE 
    public string ValidateAndGetRole(string token)
    {
        var principal = Validate(token);
        return principal.FindFirst(ClaimTypes.Role)?.Value;
    }
}