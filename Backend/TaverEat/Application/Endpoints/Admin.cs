using Infrastructure.DTO;
using Infrastructure.Interfaces;
using API.Services;

namespace Application.Endpoints;

public static class AdminEndpoint
{
    private static bool IsAdmin(string? role)
    {
        return role?.Trim().ToLower() == "admin";
    }
    public static void MapAdminEndpoints(this WebApplication app)
    {
        // GET ALL ADMIN COMANDES
        app.MapGet("/admin/comandes", (
            HttpContext http,
            JwtService jwt,
            IClientRepository clientRepo,
            IAdminComandaRepository repo
        ) =>
        {
            var token = http.Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

            var email = jwt.ValidateAndGetEmail(token);

            var user = clientRepo.GetByEmail(email);

            if (user is null)
                return Results.Unauthorized();

            if (!IsAdmin(user.Role))
                return Results.Forbid();

            var comandes = repo.GetAll();

            var result = comandes.Select(c => new ComandaAdminResponse
            {
                Id = c.Id,
                Estat = c.Estat,
                Data = c.Data,
                Productes = null,
                Direccio = c.EntregaDir
            });

            return Results.Ok(result);
        });

        // UPDATE ESTAT
        app.MapPut("/admin/comandes/{id}/estat", (
            Guid id,
            UpdateEstatComandaRequest request,
            HttpContext http,
            JwtService jwt,
            IClientRepository clientRepo,
            IAdminComandaRepository repo
        ) =>
        {
            var token = http.Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

            var email = jwt.ValidateAndGetEmail(token);

            var user = clientRepo.GetByEmail(email);

            if (user is null)
                return Results.Unauthorized();

            if (!IsAdmin(user.Role))
                return Results.Forbid();

            var comanda = repo.GetById(id);

            if (comanda is null)
                return Results.NotFound();

            var validStates = new[]
            {
                "pendent",
                "preparant",
                "repartiment",
                "entregada"
            };

            if (!validStates.Contains(request.Estat.ToLower()))
                return Results.BadRequest("Estat no vàlid");

            repo.UpdateEstat(id, request.Estat);

            return Results.Ok(new
            {
                message = "Estat actualitzat"
            });
        });
    }
}