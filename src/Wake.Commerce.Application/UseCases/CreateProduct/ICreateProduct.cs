using MediatR;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.CreateProduct;

public interface ICreateProduct : IRequestHandler<CreateProductInput, ProductOutput>
{
    public new Task<ProductOutput> Handle(
        CreateProductInput input, 
        CancellationToken cancellationToken
    );
}