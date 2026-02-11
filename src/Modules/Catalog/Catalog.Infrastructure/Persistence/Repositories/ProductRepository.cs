using BuildingBlocks.Infrastructure.Persistence.Repository;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.Context;


namespace Catalog.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ProductDbContext _context) : base(_context)
    {

    }
}