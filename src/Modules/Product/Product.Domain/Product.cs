using BuildingBlocks.Domain;

namespace Product.Domain;

public class Product : AuditableEntity
{
    public string Name { get; private set; } = default!;

    private readonly List<string> _images = new();
    public IReadOnlyCollection<string> Images => _images.AsReadOnly();
    public decimal Price { get; private set; }
    public int Stock { get; private set; }

    private Product() { }

    private Product(string name, decimal price, int stock)
    {
        Name = name;
        Price = price;
        Stock = stock;
    }
    public static Product CreateProduct(
        string name,
        decimal price,
        int stock
        )
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("name can't be empty");
        }

        if (price < 0)
        {
            throw new ArgumentException("price can't be negative");
        }

        if (stock < 0)
        {
            throw new ArgumentException("stock can't be negative");
        }

        return new Product(name, price, stock);
    }

    public void AddImage(string image)
    {
        if (string.IsNullOrEmpty(image))
        {
            throw new ArgumentException("url can't be empty");
        }

        if (!Uri.TryCreate(image, UriKind.Absolute, out _))
        {
            throw new ArgumentException("invalid image url.");
        }

        if (_images.Contains(image))
        {
            throw new ArgumentException("image already added");
        }

        _images.Add(image);
    }
}
