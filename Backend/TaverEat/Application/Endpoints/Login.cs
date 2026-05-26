using Infrastructure.DTO;
using API.Services;
using Infrastructure.Interfaces;
using Domain.Entities;

namespace Application.Endpoints;

public static class LoginEndpoint
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
        // LOGIN
        app.MapPost("/login", (LoginRequest request, JwtService jwt, IClientRepository repo) =>
        {
            var client = repo.GetByEmail(request.Email);

            if (client.Contrasenya != request.Contrasenya)
                return Results.Unauthorized();

            var token = jwt.GenerateToken(client.Email, client.Role);

            return Results.Ok(new AuthResponse(
                token,
                new UserAuthResponse(
                    client.Id,
                    client.Nom,
                    client.Email,
                    client.Role
                ),
                client.Role
            ));
        });

        // ME
        app.MapGet("/auth/me", (HttpContext http, IClientRepository repo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var email = jwt.ValidateAndGetEmail(token);

            var user = repo.GetByEmail(email);

            if (user is null)
                return Results.Unauthorized();

            return Results.Ok(new
            {
                user.Nom,
                user.Email,
                user.Direccio,
                user.Role
            });
        });

        // UPDATE PROFILE
        app.MapPut("/auth/me", (HttpContext http, ClientRequest request, IClientRepository repo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var email = jwt.ValidateAndGetEmail(token);

            var user = repo.GetByEmail(email);

            if (user is null)
                return Results.Unauthorized();

            var updated = new Client(
                user.Id,
                request.Nom,
                request.Email,
                request.Direccio,
                request.Contrasenya,
                user.Role // NO se pierde role
            );

            repo.Update(updated);

            return Results.Ok(new
            {
                updated.Nom,
                updated.Email,
                updated.Direccio,
                updated.Role
            });
        });
    }
}