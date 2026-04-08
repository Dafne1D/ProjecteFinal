using API.Services;
using Microsoft.Data.SqlClient;
using Application.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Connection string
// -----------------------------
string connectionString = builder.Configuration
    .GetConnectionString("LaTaverDB") ?? "";

builder.Services.AddScoped(sp => new TaverDBConnection(connectionString));

var app = builder.Build();

// -----------------------------
// Endpoint base
// -----------------------------
app.MapGet("/", () => "API LaTaver funcionant");

// -----------------------------
// Endpoints
// -----------------------------
app.MapCategoriesEndpoint();

app.Run();