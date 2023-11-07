using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstatePropertyListingPlatform.Domain.Records;

namespace RealEstatePropertyListingPlatform.Domain.ClassValidators
{
    public class ListingValidator
    {
        private static readonly int MaxStringLength = 100;

        public static string ValidateListing(string title, Money price, string description, List<string> photos,
                       DateTime dateCreated)
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

            if ((currentError = ValidateDateCreated(dateCreated)) != null)
            {
                return currentError;
            }


            return currentError!;
        }

        private static string ValidateMoney(Money price)
        {
            if(price.Amount < 0)
            {
                return $"Price cannot be negative";
            }

            if(price.Currency.Length != 3)
            {
                return $"Currency must be 3 characters";
            }
            return null!;
        }

        private static string ValidateString(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return $"{propertyName} cannot be empty";
            }

            if (value.Length > MaxStringLength)
            {
                return $"{propertyName} cannot be longer than {MaxStringLength} characters";
            }

            return null!;
        }

        private static string ValidatePhotos(List<string> photos)
        {
            if (photos.Count == 0)
            {
                return $"Photos cannot be empty";
            }

            return null!;
        }

        private static string ValidateDateCreated(DateTime dateCreated)
        {

            if(dateCreated > DateTime.Now)
            {
                return $"Date Created cannot be in the future";
            }

            if(dateCreated < DateTime.Now)
            {
                return $"Date Created cannot be in the past";
            }

            return null!;
        }

    }
}
