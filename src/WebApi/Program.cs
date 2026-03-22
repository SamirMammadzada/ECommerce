using BuildingBlocks.Application;
using BuildingBlocks.Application.Abstractions;
using Catalog.Application;
using Catalog.Infrastructure;
using E_Commerce.Exceptions;
using FluentValidation;
using MediatR;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCatalogInfrastructure(builder.Configuration);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(ICatalogApplicationAssemblyMarker).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(
    typeof(ICatalogApplicationAssemblyMarker).Assembly, 
    includeInternalTypes: true);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

app.UseExceptionHandler();

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

app.MapGet("product",
    async ([AsParameters] GetProductByIdQuery request, ISender sender, CancellationToken cancellationToken) =>
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
    })
    .Produces<Result<ProductDto>>()
    .DisableAntiforgery()
    ;

app.MapDelete("product",
    async ([AsParameters] DeleteProductCommand request, ISender sender, CancellationToken cancellationToken) =>
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
    })
    .Produces<Result>()
    .DisableAntiforgery()
    ;

app.MapPut("product",
    async (UpdateProductCommand request, ISender sender, CancellationToken cancellationToken) =>
    {
        var result = await sender.Send(request, cancellationToken);
        return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
    })
    .Produces<Result>()
    .DisableAntiforgery()
    ;

app.Run();



