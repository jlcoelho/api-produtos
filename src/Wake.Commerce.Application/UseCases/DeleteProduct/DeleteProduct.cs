
using MediatR;
using Wake.Commerce.Application.Interfaces;
using Wake.Commerce.Domain.Repository;

namespace Wake.Commerce.Application.UseCases.DeleteProduct;

public class DeleteProduct : IDeleteProduct
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProduct(IProductRepository productRepository, IUnitOfWork unitOfWork)
        => (_productRepository, _unitOfWork) = (productRepository, unitOfWork);

    public async Task<Unit> Handle(DeleteProductInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        await _productRepository.Delete(product, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}
