
using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories;

public interface IProductRepository
{
    public Task AddAsync(Product entity, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task UpdateAsync(Product entity, CancellationToken cancellationToken);
}
