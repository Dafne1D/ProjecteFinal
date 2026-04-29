using TaverEat.Repository;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.DTO;

namespace Application.Endpoints;
public static class ProductEndpoint
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        // GET productsBycategoia_nom
            app.MapGet("/products/category/{categoria_nom}", (string categoria_nom,[FromServices] IProductRepository repo) =>
                Results.Ok(repo.GetByCategoriaNom(categoria_nom)));

        // GET imgs for the products
        app.MapGet("/products/category/{categoria_nom}/full", (string categoria_nom,[FromServices] IProductRepository repo) =>
        {
            var results = repo.GetProductsWithImagesByCategoriaNom(categoria_nom);
            var response = results.Select(p => ProductResponse.FromProduct(p.product, p.imgUrl));
            return Results.Ok(response);
        });

        // GET products search
        app.MapGet("/products/search", (string q,[FromServices] IProductRepository repo) =>
        {
            var results = repo.SearchProductsWithImages(q);
            var response = results.Select(p => ProductResponse.FromProduct(p.product, p.imgUrl));
            return Results.Ok(response);
        });
    }  
}