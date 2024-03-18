
using Wake.Commerce.Application.Interfaces;
using Wake.Commerce.Application.UseCases.Common;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.Repository;

namespace Wake.Commerce.Application.UseCases.CreateProduct;

public class CreateProduct : ICreateProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProduct(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductOutput> Handle(
        CreateProductInput input, 
        CancellationToken cancellationToken)
    {
        var product = new Product(input.Name, input.Stock, input.Price);

        await _productRepository.Insert(product, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return ProductOutput.FromProduct(product);
    }
}