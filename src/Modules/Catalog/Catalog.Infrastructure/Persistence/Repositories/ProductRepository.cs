using BuildingBlocks.Infrastructure.Persistence.Context;
using BuildingBlocks.Infrastructure.Persistence.Repository;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;


namespace Catalog.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext _context) : base(_context)
    {

    }
}