namespace ForumSystem.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class EgnValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Value cannot be null");
            }

            var valueAsString = value.ToString();

            if (!Regex.IsMatch(valueAsString, "[0-9]{10}"))
            {
                return new ValidationResult("Personal identification number must contains 10 digits.");
            }

            var weight = new[] { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            var checksum = 0;

            for (var i = 0; i < 9; i++)
            {
                checksum += (valueAsString[i] - '0') * weight[i];
            }

            var lastDigit = checksum % 11;

            if (lastDigit == 10)
            {
                lastDigit = 0;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (valueAsString[9] - '0' != lastDigit)
            {
                return new ValidationResult("Personal identification number is invalid");
            }

            return ValidationResult.Success;
        }
    }
}
