
using MediatR;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.UpdateProduct;

public interface IUpdateProduct
    : IRequestHandler<UpdateProductInput, ProductOutput>
{}