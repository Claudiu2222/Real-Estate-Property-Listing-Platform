using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Domain.ClassValidators
{
    public class UserValidator
    {
        private static readonly int MaxStringLength = 100;

        public static string ValidateUser(string email, string password, string LastName, string FirstName)
        {

            string currentError;
            if ((currentError = ValidateString(email, nameof(email))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(password, nameof(password))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(LastName, nameof(LastName))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(FirstName, nameof(FirstName))) != null)
            {
                return currentError;
            }

            return currentError!;
        }

        public static string ValidateString(string value, string propertyName)
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
    }
}
