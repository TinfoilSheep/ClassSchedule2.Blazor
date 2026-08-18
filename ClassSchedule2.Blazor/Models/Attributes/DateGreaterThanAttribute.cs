using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Attributes
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var comparisonPropertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (comparisonPropertyInfo == null)
            {
                return new ValidationResult($"Egenskaben '{_comparisonProperty}' blev ikke fundet.");
            }

            var comparisonValue = comparisonPropertyInfo.GetValue(validationContext.ObjectInstance);

            if (value is DateOnly endDate && comparisonValue is DateOnly startDate)
            {
                if (endDate <= startDate)
                {
                    return new ValidationResult(ErrorMessage ?? "Slutdato skal være efter startdato.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
