using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using Infrastructure.Interfaces;

namespace Application.Endpoints;

public static class LoginEndpoint
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/login", ([FromBody] LoginRequest request, [FromServices] JwtService jwtService, [FromServices] IClientRepository repo) =>
        {
            var client = repo.GetByEmail(request.Email);

            if (client is null)
                return Results.Unauthorized();

            if (client.Contrasenya != request.Contrasenya)
                return Results.Unauthorized();

            var token = jwtService.GenerateToken(client.Email);

            return Results.Ok(new AuthResponse(token, client.Email));
        });
    }
}