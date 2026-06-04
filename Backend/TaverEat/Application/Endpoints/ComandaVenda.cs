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
        app.MapPost("/cart/add", (HttpContext http, AfegirProducteRequest request, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

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

            return Results.Ok(new { success = true });
        });

        app.MapGet("/cart", (HttpContext http, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

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

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id);
            if (comanda is null)
                return Results.Ok(new { success = false });

            var linea = repo.GetLinea(comanda.Id, producteId);

            if (linea is null)
                return Results.Ok(new { success = false });

            if (quantitat <= 0)
            {
                repo.DeleteLinea(linea.Id);
                return Results.Ok(new { success = true, deleted = true });
            }

            linea.Quantitat = quantitat;
            repo.UpdateLinea(linea);

            return Results.Ok(new { success = true });
        });

        app.MapDelete("/cart/item/{producteId}", (HttpContext http, Guid producteId, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id);

            if (comanda is not null)
            {
                var linea = repo.GetLinea(comanda.Id, producteId);

                if (linea is not null)
                    repo.DeleteLinea(linea.Id);
            }

            return Results.Ok(new { success = true });
        });

        app.MapPost("/cart/checkout", (HttpContext http, IComandaVendaRepository repo, IClientRepository clientRepo, JwtService jwt) =>
        {
            var token = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (string.IsNullOrWhiteSpace(token))
                return Results.Unauthorized();

            var email = jwt.ValidateAndGetEmail(token);
            var client = clientRepo.GetByEmail(email);

            var comanda = repo.GetComandaActivaByClient(client.Id);

            if (comanda is null)
                return Results.Ok(new { success = false });

            repo.SetEntregaDir(comanda.Id, client.Direccio);
            repo.ConfirmarComanda(comanda.Id);

            return Results.Ok(new
            {
                success = true,
                message = "Comanda confirmada",
                direccio = client.Direccio
            });
        });
    }
}