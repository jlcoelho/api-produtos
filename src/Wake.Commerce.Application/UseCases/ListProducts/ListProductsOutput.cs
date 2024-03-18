using Wake.Commerce.Application.Common;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.Application.UseCases.ListProducts;

public class ListProductsOutput
    : PaginatedListOutput<ProductOutput>
{
    public ListProductsOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<ProductOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}
