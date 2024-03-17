namespace Wake.Commerce.Application.UseCases.CreateProduct;

public interface ICreateProduct
{
    public Task<CreateProductOutput> Execute(
        CreateProductInput input, 
        CancellationToken cancellationToken
    );
}