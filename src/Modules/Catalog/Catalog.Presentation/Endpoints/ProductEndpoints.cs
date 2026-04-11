using BuildingBlocks.Application;
using Catalog.Application;
using Catalog.Application.Commands;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Presentation.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("products").WithTags("Products");

        group.MapPost("",
            async (CreateProductCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
            })
            .Produces<Result>()
            .DisableAntiforgery();

        group.MapGet("{Id}",
            async ([AsParameters] GetProductByIdQuery request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
            })
            .Produces<Result<ProductDto>>()
            .DisableAntiforgery();

        group.MapGet("",
            async ([AsParameters] GetProductsQuery request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
            })
            .Produces<Result<List<ProductDto>>>()
            .DisableAntiforgery();

        group.MapDelete("{Id}",
            async ([AsParameters] DeleteProductCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
            })
            .Produces<Result>()
            .DisableAntiforgery();

        group.MapPut("",
            async (UpdateProductCommand request, ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(request, cancellationToken);
                return result.IsSuccessful ? Results.Ok(result) : Results.BadRequest(result);
            })
            .Produces<Result>()
            .DisableAntiforgery();
    }
}