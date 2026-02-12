using BuildingBlocks.Application;
using Catalog.Application;
using Catalog.Infrastructure;
using MediatR;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCatalogInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference();
app.UseHttpsRedirection();

app.MapGet("/", () => Results.Redirect("/scalar"));


app.MapPost("product",
    async (CreateProductCommand request, ISender sender, CancellationToken cancellationToken) =>
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
    })
    .Produces<Result>()
    .DisableAntiforgery()
    ;

app.Run();



