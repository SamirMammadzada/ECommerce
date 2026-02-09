using MediatR;
using BuildingBlocks.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Application;

public sealed record CreateProductCommand(
    string name,
    List<string>? images,
    decimal price,
    int stock
    ) : IRequest<Result>;


internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository
    ) : IRequestHandler<CreateProductCommand, Result>
{
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        Product product = Product.Create(request.name, request.price, request.stock);

        if(request.images is not null && request.images.Count > 0)
        {
            foreach(var x in request.images)
            {
                product.AddImage(x);
            }
        }

        await productRepository.AddAsync(product, cancellationToken);
        await productRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}




