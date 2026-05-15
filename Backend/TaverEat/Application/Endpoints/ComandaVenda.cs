using API.Services;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using Infrastructure.DTO;

namespace Application.Endpoints;

public static class ComandaVendaEndpoint
{
    public static void MapComandaVendaEndpoints(this WebApplication app)
    {
        // GET comanda activa de client
        app.MapGet("/comandaVenda/{clientId}", (Guid clientId, [FromServices] IComandaVendaRepository repo) =>
        {
           var comanda = repo.GetComandaActivaByClient(clientId);
           if (comanda is null)
                return Results.NotFound("No hi ha cap comanda activa");

            return Results.Ok(ComandaVendaResponse.FromComandaVenda(comanda));
        }
        );

        // POST crear comanda nova
        app.MapPost("/comandaVenda/{clientId}", (Guid clientId, [FromServices] IComandaVendaRepository repo) =>
        {
            var comanda = repo.CreateComanda(clientId);
            return Results.Ok(ComandaVendaResponse.FromComandaVenda(comanda));
        });
    }
}