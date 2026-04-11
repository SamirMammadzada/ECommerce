using BuildingBlocks.Application;
using BuildingBlocks.Application.Pagination;
using Catalog.Domain.Repositories;

namespace Catalog.Application.Queries;

public record GetProductsQuery(int PageNumber, int PageSize) : PagedQuery(PageNumber, PageSize), IQuery<PagedResult<ProductDto>>;

internal sealed class GetProductsQueryHandler(
    IProductRepository productRepository
) : IQueryHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    public async Task<Result<PagedResult<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var (products, totalCount) = await productRepository.ListOrderedByPriceAsync(
            request.PageNumber, 
            request.PageSize,
            cancellationToken
            );

        var dtos = products
            .Select(p =>
                new ProductDto(
                    p.Name, 
                    p.Images?.ToList() ?? null, 
                    p.Price, 
                    p.Stock
                    )
                )
            .ToList();

        var pagedResult = new PagedResult<ProductDto>(dtos, request.PageSize, request.PageNumber, totalCount);

        return Result<PagedResult<ProductDto>>.Success(pagedResult);
    }
}
