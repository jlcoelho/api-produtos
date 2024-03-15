using Wake.Commerce.Domain.SeedWork;

namespace Wake.Commerce.Domain.Entity;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public int Stock { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }


    public Product(string name, int stock, decimal price) : base()
    {
        Name = name;
        Stock = stock;
        Price = price;
        CreatedAt = DateTime.Now;
    }
}