
using Wake.Commerce.Application.UseCases.Common;
using Wake.Commerce.Domain.Repository;

namespace Wake.Commerce.Application.UseCases.GetProduct;

public class GetProduct : IGetProduct
{
    private readonly IProductRepository _productRepository;

    public GetProduct(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductOutput> Handle(
        GetProductInput request, 
        CancellationToken cancellationToken
    )
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        return ProductOutput.FromProduct(product);
    }
}