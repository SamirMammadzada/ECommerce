using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext _context) : base(_context)
    {

    }
}