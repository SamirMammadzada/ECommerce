using BuildingBlocks.Domain;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<(List<Product>, int)> ListOrderedByPriceAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
