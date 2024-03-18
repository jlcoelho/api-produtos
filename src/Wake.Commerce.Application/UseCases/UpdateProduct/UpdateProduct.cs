
using Wake.Commerce.Application.Interfaces;
using Wake.Commerce.Application.UseCases.Common;
using Wake.Commerce.Domain.Repository;

namespace Wake.Commerce.Application.UseCases.UpdateProduct;

public class UpdateProduct : IUpdateProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProduct(
        IProductRepository productRepository, 
        IUnitOfWork unitOfWork)
        => (_productRepository, _unitOfWork) 
            = (productRepository, unitOfWork);

    public async Task<ProductOutput> Handle(UpdateProductInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        
        if (request.Name != null) {
            product.UpdateName(request.Name);
        }

        if (request.Stock != null) {
            product.UpdateStock((int)request.Stock);
        }

        if (request.Price != null) {
            product.UpdatePrice((decimal)request.Price);
        }

        await _productRepository.Update(product, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return ProductOutput.FromProduct(product);
    }
}