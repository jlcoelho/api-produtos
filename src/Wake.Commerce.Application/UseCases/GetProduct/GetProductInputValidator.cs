using FluentValidation;

namespace Wake.Commerce.Application.UseCases.GetProduct;

public class GetProductInputValidator 
    : AbstractValidator<GetProductInput>
{
    public GetProductInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}