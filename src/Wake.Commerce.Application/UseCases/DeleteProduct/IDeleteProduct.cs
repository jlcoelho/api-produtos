using MediatR;

namespace Wake.Commerce.Application.UseCases.DeleteProduct;
public interface IDeleteProduct 
    : IRequestHandler<DeleteProductInput, Unit>
{
}
