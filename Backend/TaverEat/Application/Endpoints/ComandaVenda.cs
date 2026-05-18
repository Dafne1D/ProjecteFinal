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
        // ADD PRODUCT TO CART
        app.MapPost("/cart/add",(HttpContext http, AfegirProducteRequest request, IComandaVendaRepository repo,IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id)
                         ?? repo.CreateComanda(client.Id);

            var linea = repo.GetLinea(comanda.Id, request.ProducteId);

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

            return Results.Ok(new { message = "Producte afegit al carrito" });
        });

        // GET CART (CURRENT USER)
        app.MapGet("/cart",(HttpContext http, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id);

            if (comanda is null)
            {
                return Results.Ok(new ComandaDetallResponse(
                    Guid.Empty,
                    new List<ComandaLineaResponse>(),
                    0
                ));
            }

            var lineas = repo.GetLineasWithProducte(comanda.Id);

            var productes = lineas.Select(linea => new ComandaLineaResponse(
                linea.producte.Id,
                linea.producte.Nom,
                linea.producte.Preu,
                linea.linea.Quantitat
            )).ToList();

            var total = lineas.Sum(x => x.producte.Preu * x.linea.Quantitat);

            return Results.Ok(new ComandaDetallResponse(
                comanda.Id,
                productes,
                total
            ));
        });

        app.MapPut("/cart/item/update", (HttpContext http, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt, Guid producteId, int quantitat) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id);
            if (comanda is null) return Results.Ok();

            var linea = repo.GetLinea(comanda.Id, producteId);

            if (linea is null)
                return Results.NotFound();

            if (quantitat <= 0)
            {
                repo.DeleteLinea(linea.Id);
                return Results.Ok();
            }

            linea.Quantitat = quantitat;
            repo.UpdateLinea(linea);

            return Results.Ok();
        });

        // REMOVE ITEM
        app.MapDelete("/cart/item/{producteId}", (HttpContext http, Guid producteId, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id);
            if (comanda is null) return Results.Ok();

            var linea = repo.GetLinea(comanda.Id, producteId);

            if (linea is not null)
            {
                repo.DeleteLinea(linea.Id);
            }

            return Results.Ok();
        });
    }
}