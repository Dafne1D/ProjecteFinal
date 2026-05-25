using Infrastructure.Repositories;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Mappers;

namespace Application.Endpoints;

public static class ClientEndpoint
{
    public static void MapClientEndpoints(this WebApplication app)
    {
        app.MapGet("/clients", ([FromServices] IClientRepository repo) =>
        {
            var clients = repo.GetAll();
            return Results.Ok(clients);
        });

        app.MapGet("/client/{id}", (Guid id, IClientRepository repo) =>
        {
            var client = repo.GetById(id);

            if (client is null)
                return Results.NotFound();

            return Results.Ok(ClientResponse.FromClient(client));
        });

        // CREATE CLIENT
        app.MapPost("/clients", (ClientRequest request, IClientRepository repo) =>
        {
            var client = ClientMapper.FromRequest(request);

            repo.Insert(client);

            return Results.Ok(ClientResponse.FromClient(client));
        });

        // UPDATE CLIENT
        app.MapPut("/clients/{id}", (Guid id, ClientRequest request, IClientRepository repo) =>
        {
            var existing = repo.GetById(id);

            if (existing is null)
                return Results.NotFound();

            var updated = new Client(
                id,
                request.Nom,
                request.Email,
                request.Direccio,
                request.Contrasenya,
                existing.Role
            );

            repo.Update(updated);

            return Results.Ok(ClientResponse.FromClient(updated));
        });

        app.MapDelete("/clients/{id}", (Guid id, IClientRepository repo) =>
        {
            var deleted = repo.Delete(id);
            return deleted ? Results.Ok() : Results.NotFound();
        });
    }
}