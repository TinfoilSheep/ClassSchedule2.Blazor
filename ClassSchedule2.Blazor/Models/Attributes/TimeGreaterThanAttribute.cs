using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Attributes
{
    public class TimeGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public TimeGreaterThanAttribute(string comparisonProperty)
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

            if (value is TimeOnly endTime && comparisonValue is TimeOnly startTime)
            {
                if (endTime <= startTime)
                {
                    return new ValidationResult(ErrorMessage ?? "Sluttidspunkt skal være efter starttidspunkt.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
