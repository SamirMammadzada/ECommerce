
using BuildingBlocks.Domain;
using BuildingBlocks.Infrastructure.Persistence;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence.Context;
using Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ProductDbContext>(options =>
        
            options.UseSqlServer(connectionString)
        );

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
