using BuildingBlocks.Domain;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace Catalog.Infrastructure.Persistence.Repositories;

public abstract class Repository<T> : IRepository<T> where T : AuditableEntity
{
    private readonly DbSet<T> _dbSet;
    public Repository (ApplicationDbContext context) 
    {
        _dbSet = context.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    { 
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }
}
