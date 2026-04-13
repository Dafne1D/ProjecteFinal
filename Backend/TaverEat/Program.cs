using API.Services;
using Application.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Serveis
// -----------------------------
builder.Services.AddScoped<TaverDBConnection>();

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
app.MapImgUrlEndpoints();

app.Run();