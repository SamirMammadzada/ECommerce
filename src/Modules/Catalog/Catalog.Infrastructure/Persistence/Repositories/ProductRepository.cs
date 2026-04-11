using BuildingBlocks.Infrastructure.Persistence.Repository;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ProductDbContext _context) : base(_context)
    {

    }

    public async Task<(List<Product>, int)> ListOrderedByPriceAsync(int pageNumber, int pageSize,CancellationToken cancellationToken)
    {
        var query = _dbSet.OrderBy(p => p.Price);
    
        var totalCount = await query.CountAsync(cancellationToken);
    
        var products = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return (products, totalCount);
    }
}