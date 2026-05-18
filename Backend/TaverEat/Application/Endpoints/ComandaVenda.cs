using API.Services;
using Domain.Entities;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Endpoints;

public static class ComandaVendaEndpoint
{
    public static void MapComandaVendaEndpoints(this WebApplication app)
    {
        app.MapPost("/comandaVenda/add-product", (
            AfegirProducteRequest request,
            [FromServices] IComandaVendaRepository repo
        ) =>
        {
            // buscar comanda activa
            var comanda = repo.GetComandaActivaByClient(request.ClientId);

            // si no existe -> crear
            if (comanda is null)
            {
                comanda = repo.CreateComanda(request.ClientId);
            }

            // buscar si ya existe linea
            var linea = repo.GetLinea(comanda.Id, request.ProducteId);

            // si existe -> sumar cantidad
            if (linea is not null)
            {
                linea.Quantitat += 1;

                repo.UpdateLinea(linea);
            }
            else
            {
                repo.AddLinea(new ComandaVendaLinea(
                    Guid.NewGuid(),
                    request.ProducteId,
                    comanda.Id,
                    1
                ));
            }

            return Results.Ok();
        });
    }
}