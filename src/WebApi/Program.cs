using Scalar.AspNetCore;
using Catalog.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCatalogInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();
app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/scalar"));

app.Run();



