namespace RealEstatePropertyListingPlatform.Domain.ClassValidators
{
    public class UserValidator
    {
        private static readonly int MaxStringLength = 100;
        private static readonly int MinPasswordLength = 8;
        public static string ValidateUser(string email, string password, string lastName, string firstName, string phoneNumber)
        {

            string currentError;
            if ((currentError = ValidateString(email, nameof(email))) != null)
            {
                return currentError;
            }
            if ((currentError = ValidateString(lastName, nameof(lastName))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(firstName, nameof(firstName))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidatePassword(password)) != null)
            {
                return currentError;
            }


            if ((currentError = ValidatePhoneNumber(phoneNumber)) != null)
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

        public static string ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Password cannot be empty";
            }

            if (password.Length < MinPasswordLength)
            {
                return $"Password must be at least {MinPasswordLength} characters";
            }

            return null!;
        }

        public static string? ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return null;
            }

            if (phoneNumber.Length != 10)
            {
                return "Phone number must be 10 digits";
            }


            return null;
        }
    }
}
