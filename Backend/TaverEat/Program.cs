using API.Services;
using Application.Endpoints;
using Infrastructure.Repositories;
using Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Serveis
// -----------------------------
builder.Services.AddScoped<TaverDBConnection>();

builder.Services.AddScoped<ICategoryRepository, CategoryADO>();
builder.Services.AddScoped<IProductRepository, ProductADO>();
builder.Services.AddScoped<IComandaVendaRepository, ComandaVendaADO>();
builder.Services.AddScoped<IClientRepository, ClientADO>();
builder.Services.AddScoped<JwtService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// -----------------------------
// Endpoints
// -----------------------------
app.MapCategoriaEndpoints();
app.MapProductEndpoints();
app.MapComandaVendaEndpoints();
app.MapClientEndpoints();
app.MapLoginEndpoints();
app.MapAdminEndpoints();
app.Run();