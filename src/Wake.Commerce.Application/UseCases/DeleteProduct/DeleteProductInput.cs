using MediatR;

namespace Wake.Commerce.Application.UseCases.DeleteProduct;

public class DeleteProductInput : IRequest<Unit>
{
    public Guid Id { get; set; }
    public DeleteProductInput(Guid id)
    {
        Id = id;
    }
}