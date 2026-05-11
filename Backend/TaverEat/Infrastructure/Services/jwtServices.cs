using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

public class JwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    } 

    public string GenerateToken(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email)
        };
    
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:JstSecretKey"]!
            )
        );

        var credentials = new SinginCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            singinCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }    
}