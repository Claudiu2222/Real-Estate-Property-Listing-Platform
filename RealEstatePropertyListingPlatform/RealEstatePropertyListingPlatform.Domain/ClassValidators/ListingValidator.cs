using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.ClassValidators
{
    public class ListingValidator
    {
        private const int MaxStringLength = 100;

        public static string ValidateListing(string title, Money price, string description, List<string> photos)
        {

            string currentError;
            if ((currentError = ValidateString(title, nameof(title))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateMoney(price)) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(description, nameof(description))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidatePhotos(photos)) != null)
            {
                return currentError;
            }
            

            return currentError!;
        }

        public static string ValidateMoney(Money price)
        {
            if(price.Amount < 0)
            {
                return $"Price cannot be negative";
            }

            return price.Currency.Length != 3 ? $"Currency must be 3 characters" : null!;
        }

        public static string ValidateString(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return $"{propertyName} cannot be empty";
            }

            return value.Length > MaxStringLength ? $"{propertyName} cannot be longer than {MaxStringLength} characters" : null!;
        }

        public static string ValidatePhotos(List<string> photos)
        {
            if (photos is { Count: < 1 })
            {
                return $"Photos cannot be empty";
            }

            return photos.Any(string.IsNullOrWhiteSpace) ? $"Photo cannot be empty" : null!;
        }
        
    }
}
