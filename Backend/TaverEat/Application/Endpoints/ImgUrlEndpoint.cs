using API.Services;
using TaverEat.Repository;

namespace Application.Endpoints;

public static class ImgUrlEndpoint
{
    public static void MapImgUrlEndpoints(this WebApplication app)
    {
        // GET product/{product_id}/image
        app.MapGet("/products/{product_id}/image", (Guid product_id, TaverDBConnection dbConn) =>
        {
            if (!dbConn.Open())
                return Results.Problem("No s'ha pogut connectar amb la base de dades");
            try
            {
                var url = ImgUrlADO.GetUrlByProductId(dbConn, product_id);
                if (url == null)
                {
                    return Results.NotFound(new { message = "No s'ha trobat la imatge d'aquest producte" });
                }
                return Results.Ok(new { url });
            }
            finally
            {
                dbConn.Close();
            }
        });
    }
}
