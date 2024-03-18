using MediatR;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.GetProduct;

public class GetProductInput : IRequest<ProductOutput>
{
    public Guid Id { get; set; }
    public GetProductInput(Guid id)
    {
        Id = id;
    }
}