

using BuildingBlocks.Domain;

namespace Catalog.Domain.Entities;

public class Category : AuditableEntity
{
    public string Name { get; protected set; } = default!;

}
