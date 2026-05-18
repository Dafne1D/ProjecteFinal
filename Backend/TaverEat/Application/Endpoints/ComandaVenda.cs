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

            // si no elineaiste -> crear
            if (comanda is null)
            {
                comanda = repo.CreateComanda(request.ClientId);
            }

            // buscar si ya elineaiste linea
            var linea = repo.GetLinea(comanda.Id, request.ProducteId);

            // si elineaiste -> sumar cantidad
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

        // GET carrito
        app.MapGet("/comandaVenda/{clientId}/lineas", (Guid clientId, IComandaVendaRepository repo) =>
        {
            var comanda = repo.GetComandaActivaByClient(clientId);

            if (comanda is null)
            {
                return Results.Ok(
                    new ComandaDetallResponse(Guid.Empty, new List<ComandaLineaResponse>())
                );
            }

            var lineas = repo.GetLineasWithProducte(comanda.Id);

            var response = new ComandaDetallResponse(
                comanda.Id,

                lineas.Select(linea =>
                    new ComandaLineaResponse(
                        linea.producte.Id,
                        linea.producte.Nom,
                        linea.producte.Preu,
                        linea.linea.Quantitat
                    )
                ).ToList()
            );

            return Results.Ok(response);
        });
    }
}