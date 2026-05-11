using Infrastructure.Repositories;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using Infrastructure.DTO;
using TaverEat.Repository;

namespace Application.Endpoints;

public static class ClietnEndpoint
{
    public static void MapClientEndpoints(this WebApplication app)
    {
        app.MapGet("/client", ([FromServices] IClientRepository repo) =>
        {
            var categories = repo.GetAll();
            return Results.Ok(categories);
        });

        app.MapGet("/client/{id}", (Guid id, [FromServices] IClientRepository repo) =>
        {
            var client = repo.GetById(id);
            if(client is null) return Results.NotFound();
            return Results.Ok(ClientResponse.FromClient(client));
        });

        app.MapPost("/clients", ([FromBody] ClientRequest request, [FromServices] IClientRepository repo) =>
        {
            var client = request.ToClient();
            repo.Insert(client);
            return Results.Ok(ClientResponse.FromClient(client));
        });

        app.MapPut("/clients/{id}", (Guid id, [FromBody] ClientRequest request, [FromServices] IClientRepository repo) =>
        {
            var existeix = repo.GetById(id);
            if (existeix is null) return Results.NotFound();

            var updated = new Client(
                id,
                request.Nom,
                request.Email,
                request.Direccio,
                request.Contrasenya
            );

            repo.Update(updated);

            return Results.Ok(ClientResponse.FromClient(updated));
        });

        app.MapDelete("/clients/{id}", (Guid id, [FromServices] IClientRepository repo) =>
        {
           var deleted = repo.Delete(id);
           return deleted ? Results.Ok() : Results.NotFound(); 
        });
    }
}