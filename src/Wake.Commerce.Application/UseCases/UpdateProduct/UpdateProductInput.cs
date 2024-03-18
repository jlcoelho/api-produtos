
using MediatR;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.UpdateProduct;

public class UpdateProductInput 
    : IRequest<ProductOutput>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Stock { get; set; }
    public decimal? Price { get; set; }

    public UpdateProductInput(
        Guid id, 
        string? name = null, 
        int? stock = null, 
        decimal? price = null)
    {
        Id = id;
        Name = name;
        Stock = stock;
        Price = price;
    }

}