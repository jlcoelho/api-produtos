using Wake.Commerce.Domain.Exceptions;

namespace Wake.Commerce.Domain.Validation;

public class DomainValidation
{
    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException(
                $"{fieldName} should not be empty or null");
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target.Length < minLength)
            throw new EntityValidationException($"{fieldName} should be at least {minLength} characters long");
    }
    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
            throw new EntityValidationException($"{fieldName} should be less or equal {maxLength} characters long");
    }

    public static void MinValue(decimal target, decimal minValue, string fieldName)
    {
        if (target < minValue)
            throw new EntityValidationException($"{fieldName} should not be less than {minValue}");
    }
}
