
using FluentValidation;

namespace Wake.Commerce.Application.UseCases.UpdateProduct;

public class UpdateProductInputValidator
    : AbstractValidator<UpdateProductInput>
{
    public UpdateProductInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}