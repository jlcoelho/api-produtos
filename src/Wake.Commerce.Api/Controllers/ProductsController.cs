using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wake.Commerce.Api.ApiModels.Request;
using Wake.Commerce.Api.ApiModels.Response;
using Wake.Commerce.Application.UseCases.Common;
using Wake.Commerce.Application.UseCases.CreateProduct;
using Wake.Commerce.Application.UseCases.DeleteProduct;
using Wake.Commerce.Application.UseCases.GetProduct;
using Wake.Commerce.Application.UseCases.ListProducts;
using Wake.Commerce.Application.UseCases.UpdateProduct;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;

namespace Wake.Commerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsControllers : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductInput input, 
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(input, cancellationToken);

        return CreatedAtAction(
            nameof(Create), 
            new { output.Id }, 
            new ApiResponse<ProductOutput>(output)
        );
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(
        [FromBody] UpdateProductApiInput input,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var updateInput = new UpdateProductInput(
            id,
            input.Name,
            input.Stock,
            input.Price
        );
        var output = await _mediator.Send(updateInput, cancellationToken);
        return Ok(new ApiResponse<ProductOutput>(output));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ProductOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        var output = await _mediator.Send(new GetProductInput(id), cancellationToken);
        return Ok(new ApiResponse<ProductOutput>(output));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
    )
    {
        await _mediator.Send(new DeleteProductInput(id), cancellationToken);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListProductsOutput), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(
        CancellationToken cancellationToken,        
        [FromQuery] int? page = null,
        [FromQuery(Name = "per_page")] int? perPage = null,
        [FromQuery] string? search = null,
        [FromQuery] string? sort = null,
        [FromQuery] SearchOrder? dir = null
    )
    {
        var input = new ListProductsInput();
        if (page is not null) input.Page = page.Value;
        if (perPage is not null) input.PerPage = perPage.Value;
        if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
        if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
        if (dir is not null) input.Dir = dir.Value;
        
        var output = await _mediator.Send(input, cancellationToken);
        return Ok(
            new ApiResponseList<ProductOutput>(output)
        );
    }
}