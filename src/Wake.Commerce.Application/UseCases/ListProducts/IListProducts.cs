using MediatR;

namespace Wake.Commerce.Application.UseCases.ListProducts;

public interface IListProducts
    : IRequestHandler<ListProductsInput, ListProductsOutput>
{}
