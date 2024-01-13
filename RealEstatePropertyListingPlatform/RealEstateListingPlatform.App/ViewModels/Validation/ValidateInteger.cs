using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class ValidateInteger : ValidationAttribute
    {

        private readonly int minValue;
        private readonly int maxValue;

        public ValidateInteger(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && value is int intValue)
            {
                if (intValue < minValue)
                {
                    return new ValidationResult($"Value is too low");
                }
                else if (intValue > maxValue)
                {
                    return new ValidationResult($"Value is too high");
                }

            }

            return ValidationResult.Success;
        }

    }
}
