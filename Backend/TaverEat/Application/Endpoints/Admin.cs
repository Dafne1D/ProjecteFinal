using Infrastructure.DTO;
using Infrastructure.Interfaces;

namespace Application.Endpoints;

public static class AdminEndpoint
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        // GET ALL COMMANDES
        app.MapGet("/admin/comandes", (
            IComandaVendaRepository repo
        ) =>
        {
            var comandes = repo.GetAll();

            return Results.Ok(comandes);
        });

        // UPDATE ESTAT
        app.MapPut("/admin/comandes/{id}/estat", (
            Guid id,
            UpdateEstatComandaRequest request,
            IComandaVendaRepository repo
        ) =>
        {
            repo.UpdateEstat(id, request.Estat);

            return Results.Ok(new
            {
                message = "Estat actualitzat"
            });
        });
    }
}