using TaverEat.Repository;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Application.Endpoints;
public static class ProductEndpoint
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        // GET productsByCategoryNom
        app.MapGet("/products/category/{categoryNom}", (string categoryNom, TaverDBConnection dbConn) =>
        {
            if (!dbConn.Open())
                return Results.Problem("No s'ha pogut connectar amb la base de dades");
            try
            {
                var products = ProductADO.GetByCategoryNom(dbConn, categoryNom);
                return Results.Ok(products);
            }
            finally
            {
                dbConn.Close();
            }
        });
    }  
}