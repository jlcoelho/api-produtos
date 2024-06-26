using MediatR;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.CreateProduct;

public class CreateProductInput : IRequest<ProductOutput>
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }

    public CreateProductInput(
        string name,
        int stock,
        decimal price)
    {
        Name = name;
        Stock = stock;
        Price = price;
    }
}
