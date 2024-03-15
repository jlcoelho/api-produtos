using Wake.Commerce.Domain.SeedWork;
using Wake.Commerce.Domain.Validation;

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
        Validate();
    }

    public void UpdateName(string name)
    {
        Name = name;
        Validate();
    }

    public void UpdateStock(int stock)
    {
        Stock = stock;
        Validate();
    }

    public void UpdatePrice(decimal price)
    {
        Price = price;
        Validate();
    }

    public void Validate()
    {

        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        DomainValidation.MinLength(Name, 4, nameof(Name));
        DomainValidation.MaxLength(Name, 255, nameof(Name));
        DomainValidation.MinValue(Stock, 0, nameof(Stock));
        DomainValidation.MinValue(Price, 0, nameof(Price));
    }
}


