using Infrastructure.Repositories;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace Application.Endpoints;

public static class CategoriaEndpoints
{
    public static void MapCategoriaEndpoints(this WebApplication app)
    {
        app.MapGet("/categories", ([FromServices] ICategoryRepository repo) =>
        {
            var categories = repo.GetAll();
            return Results.Ok(categories);
        });
    }
}