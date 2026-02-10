namespace BuildingBlocks.Domain;

public interface IRepository<T> where T : AuditableEntity
{
    public Task AddAsync(T entity, CancellationToken cancellationToken);
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
}
