using Infrastructure.Repositories;
using API.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Infrastructure.Interfaces;

namespace Application.Endpoints;

public static class ClietnEndpoint
{
    public static void MapClientEndpoints(this WebApplication app)
    {
        app.MapGet("/client", ([FromServices] IClientRepository repo) =>
        {
            var categories = repo.GetAll();
            return Results.Ok(categories);
        });
    }
}