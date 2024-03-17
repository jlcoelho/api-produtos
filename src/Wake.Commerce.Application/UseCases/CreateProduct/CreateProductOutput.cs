namespace Wake.Commerce.Application.UseCases.CreateProduct;

public class CreateProductOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }

    public CreateProductOutput(Guid id, string name, int stock, decimal price, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Stock = stock;
        Price = price;
        CreatedAt = createdAt;
    }
}