
using BuildingBlocks.Application;
using BuildingBlocks.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using FluentValidation;

namespace Catalog.Application.Commands;

public record UpdateProductCommand : ICommand
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public List<string>? Images { get; set; } = new();
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
}

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty")
            .MaximumLength(200)
            .WithMessage("Maximum length of product name is 200")
            .When(e => e.Name != null);

        RuleForEach(e => e.Images)
            .NotEmpty()
            .WithMessage("Image url can't be empty")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Image urls are invalid")
            .When(e => e.Images != null);

        RuleFor(e => e.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price can't be negative")
            .When(e => e.Price != null);

        RuleFor(e => e.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock can't be negative")
            .When(e => e.Stock != null);
    }
}

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

        if(product is null || product.IsDeleted)
        {
            return Result.Failure("Product does not exists.");
        }

        product.Update(request.Name, request.Price, request.Stock, request.Images);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}