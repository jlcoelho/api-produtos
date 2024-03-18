using MediatR;
using Wake.Commerce.Application.Common;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;

namespace Wake.Commerce.Application.UseCases.ListProducts;

public class ListProductsInput
    : PaginatedListInput, IRequest<ListProductsOutput>
{
    public ListProductsInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
    ) : base(page, perPage, search, sort, dir)
    { }
    
    public ListProductsInput() 
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}