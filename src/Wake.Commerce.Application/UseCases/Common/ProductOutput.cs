using Wake.Commerce.Domain.Entity;

namespace Wake.Commerce.Application.UseCases.Common;

public class ProductOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProductOutput(Guid id, string name, int stock, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Stock = stock;
        Price = price;
        CreatedAt = createdAt;
    }

    public static ProductOutput FromProduct(Product product)
    {
        return new ProductOutput(
            product.Id,
            product.Name,
            product.Stock,
            product.Price,
            product.CreatedAt
        );
    }
}