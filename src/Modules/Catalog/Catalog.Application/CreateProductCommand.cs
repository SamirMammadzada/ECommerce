using MediatR;
using BuildingBlocks.Domain;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using BuildingBlocks.Application;
using FluentValidation;

namespace Catalog.Application;

public class CreateProductCommand : IRequest<Result>
{
    public string Name { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty")
            .MaximumLength(200)
            .WithMessage("Maximum length of product name is 200");

        RuleForEach(e => e.Images)
            .NotEmpty()
            .WithMessage("Image url can't be empty")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Image urls are invalid")
            .When(e => e.Images != null);

        RuleFor(e => e.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price can't be negative");

        RuleFor(e => e.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock can't be negative");


    }
}

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateProductCommand, Result>
{
    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        Product product = Product.Create(request.Name, request.Price, request.Stock);

        if(request.Images is not null && request.Images.Count > 0)
        {
            foreach(var x in request.Images)
            {
                product.AddImage(x);
            }
        }

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}




