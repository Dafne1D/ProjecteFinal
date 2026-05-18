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
        // ADD PRODUCT
        app.MapPost("/cart/add", (HttpContext http,AfegirProducteRequest request, [FromServices] IComandaVendaRepository repo, [FromServices] IClientRepository clientRepo, [FromServices] JwtService jwt) =>
        {
            // token
            var token = http.Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            // email desde jwt
            var email = jwt.ValidateAndGetEmail(token);

            // client logged
            var client = clientRepo.GetByEmail(email);

            // buscar carrito activo
            var comanda = repo.GetComandaActivaByClient(client.Id);

            // si no existe -> crear
            if (comanda is null)
            {
                comanda = repo.CreateComanda(client.Id);
            }

            // buscar linea existente
            var linea = repo.GetLinea(comanda.Id, request.ProducteId);

            // sumar cantidad
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

        // GET CART
        app.MapGet("/cart", (
            HttpContext http,
            [FromServices] IComandaVendaRepository repo,
            [FromServices] IClientRepository clientRepo,
            [FromServices] JwtService jwt
        ) =>
        {
            // token
            var token = http.Request.Headers.Authorization
                .ToString()
                .Replace("Bearer ", "");

            // email
            var email = jwt.ValidateAndGetEmail(token);

            // user
            var client = clientRepo.GetByEmail(email);

            // carrito activo
            var comanda = repo.GetComandaActivaByClient(client.Id);

            // carrito vacío
            if (comanda is null)
            {
                return Results.Ok(
                    new ComandaDetallResponse(
                        Guid.Empty,
                        new List<ComandaLineaResponse>(),
                        0
                    )
                );
            }

            // lineas
            var lineas = repo.GetLineasWithProducte(comanda.Id);

            // productos
            var productes = lineas.Select(x =>
                new ComandaLineaResponse(
                    x.producte.Id,
                    x.producte.Nom,
                    x.producte.Preu,
                    x.linea.Quantitat
                )
            ).ToList();

            // total
            decimal total = lineas.Sum(x =>
                x.producte.Preu * x.linea.Quantitat
            );

            // response
            var response = new ComandaDetallResponse(
                comanda.Id,
                productes,
                total
            );

            return Results.Ok(response);
        });
    }
}