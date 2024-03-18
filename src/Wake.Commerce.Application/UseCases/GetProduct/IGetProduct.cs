using MediatR;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.GetProduct;

public interface IGetProduct: IRequestHandler<GetProductInput, ProductOutput>
{}
