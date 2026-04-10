namespace Application.Endpoints;
using TaverEat.Repository;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public static class CategoriaEndpoints
{
    public static void MapCategoriaEndpoints(this WebApplication app)
    {
        // GET categories
        app.MapGet("/categories", (TaverDBConnection dbConn) =>
        {
            // Abrir la conexión
            if (!dbConn.Open())
                return Results.Problem("No s'ha pogut connectar amb la base de dades");
            try
            {
                var categorias = CategoriaADO.GetAll(dbConn);
                return Results.Ok(categorias);
            }
            finally
            {
                dbConn.Close();
            }
        });
    }  
}