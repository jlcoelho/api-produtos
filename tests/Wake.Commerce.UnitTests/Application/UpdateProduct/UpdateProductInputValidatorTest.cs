
using FluentAssertions;
using FluentValidation;
using Wake.Commerce.Application.UseCases.UpdateProduct;
using Xunit;

namespace Wake.Commerce.UnitTests.Application.UpdateProduct;

[Collection(nameof(UpdateProductTestFixture))]
public class UpdateProductInputValidatorTest
{
    private readonly UpdateProductTestFixture _fixture;

    public UpdateProductInputValidatorTest(UpdateProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DontValidateWhenEmptyGuid))]
    [Trait("Application", "UpdateProductInputValidator - Use Cases")]
    public void DontValidateWhenEmptyGuid()
    {
        ValidatorOptions.Global.LanguageManager.Enabled = false;
        var input = _fixture.GetValidInput(Guid.Empty);
        var validator = new UpdateProductInputValidator();

        var validateResult = validator.Validate(input);

        validateResult.Should().NotBeNull();
        validateResult.IsValid.Should().BeFalse();
        validateResult.Errors.Should().HaveCount(1);
        validateResult.Errors[0].ErrorMessage
            .Should().Be("'Id' must not be empty.");
    }


    [Fact(DisplayName = nameof(ValidateWhenValid))]
    [Trait("Application", "UpdateProductInputValidator - Use Cases")]
    public void ValidateWhenValid()
    {
        var input = _fixture.GetValidInput();
        var validator = new UpdateProductInputValidator();

        var validateResult = validator.Validate(input);

        validateResult.Should().NotBeNull();
        validateResult.IsValid.Should().BeTrue();
        validateResult.Errors.Should().HaveCount(0);
    }
}