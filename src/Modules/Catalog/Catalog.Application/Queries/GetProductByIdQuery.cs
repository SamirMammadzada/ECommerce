using BuildingBlocks.Application;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;


namespace Catalog.Application.Queries;

public record GetProductByIdQuery(Guid id) : IQuery<ProductDto>;

public record ProductDto(
    string Name,
    List<string>? Images,
    decimal Price,
    int Stock
);

internal sealed class GetProductByIdQueryHandler(
    IProductRepository productRepository
    ) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByIdAsync(request.id, cancellationToken);

        if (product is null || product.IsDeleted)
        {
            return Result<ProductDto>.Failure("Product doesn't exist");
        }

        ProductDto productDto = new ProductDto(product.Name, product.Images.ToList() ?? [], product.Price, product.Stock);

        return Result<ProductDto>.Success(productDto);

    }
}