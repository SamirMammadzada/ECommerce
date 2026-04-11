using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Application.Commands;

public record DeleteProductCommand(Guid Id) : ICommand;

internal sealed class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {

        Product? product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if(product is null || product.IsDeleted)
        {
            return Result.Failure("Product does not exists.");
        }

        product.Delete();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}