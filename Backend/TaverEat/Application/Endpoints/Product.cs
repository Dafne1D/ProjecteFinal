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
        // GET productsBycategoia_nom
        app.MapGet("/products/category/{categoia_nom}", (string categoia_nom, TaverDBConnection dbConn) =>
        {
            if (!dbConn.Open())
                return Results.Problem("No s'ha pogut connectar amb la base de dades");
            try
            {
                var products = ProductADO.GetByCategoriaNom(dbConn, categoia_nom);
                return Results.Ok(products);
            }
            finally
            {
                dbConn.Close();
            }
        });   

        // GET imgs for the products
        app.MapGet("/products/category/{categoria_nom}/full", (string categoria_nom, TaverDBConnection dbConn) =>
        {
            if (!dbConn.Open())
                return Results.Problem("No s'ha pogut connectar amb la base de dades");
            try
            {
                var productsAndImages = ProductADO.GetProductsWithImagesByCategoriaNom(dbConn, categoria_nom);
                var productResponses = productsAndImages.Select(p => 
                    TaverEat.Infrastructure.DTO.ProductResponse.FromProduct(p.product, p.imgUrl)
                ).ToList();

                return Results.Ok(productResponses);
            }
            finally
            {
                dbConn.Close();
            }
        });
    }  
}