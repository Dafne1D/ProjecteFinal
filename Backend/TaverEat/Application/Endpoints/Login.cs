using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using API.Services;
using Infrastructure.Interfaces;
using Domain.Entities;


namespace Application.Endpoints;

public static class LoginEndpoint
{
    public static void MapLoginEndpoints(this WebApplication app)
    {
       app.MapPost("/login", (LoginRequest request, JwtService jwtService, IClientRepository repo) =>
        {
            try
            {
                var client = repo.GetByEmail(request.Email);

                if (client.Contrasenya != request.Contrasenya)
                    return Results.Unauthorized();

                var token = jwtService.GenerateToken(client.Email);

                return Results.Ok(new AuthResponse(token, client.Email));
            }
            catch
            {
                return Results.Unauthorized();
            }
        });
        
        app.MapGet("/auth/me", (HttpContext http, IClientRepository repo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var email = jwt.ValidateAndGetEmail(token);

            var user = repo.GetByEmail(email);

            return Results.Ok(new
            {
                user.Nom,
                user.Email,
                user.Direccio
            });
        });

        app.MapPut("/auth/me", (HttpContext http, ClientRequest request, IClientRepository repo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            var email = jwt.ValidateAndGetEmail(token);

            var user = repo.GetByEmail(email);

            var updated = new Client(
                user.Id,
                request.Nom,
                request.Email,
                request.Direccio,
                user.Contrasenya 
            );

            repo.Update(updated);

            return Results.Ok(new
            {
                updated.Nom,
                updated.Email,
                updated.Direccio
            });
        });
    }
}