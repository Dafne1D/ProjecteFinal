using API.Services;
using Application.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Servicios
// -----------------------------
builder.Services.AddScoped<TaverDBConnection>();

// (Opcional pero MUY recomendado para frontend)
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

// -----------------------------
// Middlewares
// -----------------------------
app.UseCors("AllowFrontend");

// -----------------------------
// Endpoint base
// -----------------------------
app.MapGet("/", () => "API LaTaver funcionant");

// -----------------------------
// Endpoints
// -----------------------------
app.MapCategoriaEndpoints();

app.Run();