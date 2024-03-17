
using Wake.Commerce.Application.Interfaces;
using Wake.Commerce.Domain;
using Wake.Commerce.Domain.Entity;

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

    public async Task<CreateProductOutput> Execute(
        CreateProductInput input, 
        CancellationToken cancellationToken)
    {
        var product = new Product(input.Name, input.Stock, input.Price);

        await _productRepository.Insert(product, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return new CreateProductOutput(
            product.Id,
            product.Name,
            product.Stock,
            product.Price,
            product.CreatedAt
        );
    }
}