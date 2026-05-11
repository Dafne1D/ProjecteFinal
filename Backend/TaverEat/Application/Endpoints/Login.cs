using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;


namespace Application.Endpoints;

public static class LoginEndpoint
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/login", ([FromBody] LoginRequest request, [FromServices] JwtServices jwtService ) =>
        {
            
        });
    }
}