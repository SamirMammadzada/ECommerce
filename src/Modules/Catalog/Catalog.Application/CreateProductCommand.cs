using MediatR;
using BuildingBlocks.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Application;

public class CreateProductCommand : IRequest<Result>
{
    public string name { get; set; } = string.Empty;
    public List<string> images { get; set; } = new();
    public decimal price { get; set; }
    public int stock { get; set; }
}


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

        return Result.Success();
    }
}




