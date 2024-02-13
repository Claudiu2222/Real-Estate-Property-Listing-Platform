using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.ClassValidators
{
    public class PropertyValidator
    {
        private static readonly int MaxStringLength = 100;

        public static string ValidateProperty(string streetName, string city, string region, string postalCode,
            string country, PropertyType propertyType, int numberOfRooms, int numberOfBathrooms, int floor,
           int numberOfFloors, int squareFeet, string Longitude, string Latitude)
        {

            string currentError;
            if ((currentError = ValidatePropertyType(propertyType)) != null)
            {
                return currentError;
            }
            if ((currentError = ValidateString(streetName, nameof(streetName))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(city, nameof(city))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(region, nameof(region))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(postalCode, nameof(postalCode))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(country, nameof(country))) != null)
            {
                return currentError;
            }

            if ((currentError = validateFloor(floor, numberOfFloors,propertyType)) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateRooms(numberOfRooms, propertyType)) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateBathrooms(numberOfBathrooms, propertyType)) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateSquareFeet(squareFeet)) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(Longitude, nameof(Longitude))) != null)
            {
                return currentError;
            }

            if ((currentError = ValidateString(Latitude, nameof(Latitude))) != null)
            {
                return currentError;
            }
            return currentError!;
        }

        public static string ValidateSquareFeet(int squareFeet)
        {
            if (squareFeet < 1)
            {
                return $"Square feet must be greater than 0";
            }

            return null!;
        }

        public static string ValidateRooms(int numberOfRooms, PropertyType propertyType)
        {
            if (propertyType == PropertyType.Land)
            {
                if (numberOfRooms != 0)
                {
                    return $"Number of rooms must be 0 for {propertyType}";
                }
                return null!; //if number of rooms is 0 for land, than it's valid
            }
                
            if(numberOfRooms < 1)
            {
                return $"Number of rooms must be greater than 0 for {propertyType}";
            }   

            return null!;
        }

        public static string ValidateBathrooms(int numberOfBathrooms, PropertyType propertyType)
        {
            if (propertyType == PropertyType.Land || propertyType == PropertyType.Garage)
            {
                if (numberOfBathrooms != 0)
                {
                    return $"Number of bathrooms must be 0 for {propertyType}";
                }
                return null!; //if number of bathrooms is 0 for land or garage, than it's valid
            }

            if (numberOfBathrooms < 1)
            {
                return $"Number of bathrooms must be greater than 0 for {propertyType}";
            }

            return null!;
        }
        public static string ValidateString(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"{propertyName} cannot be empty";
            if (value.Length > MaxStringLength)
                return $"{propertyName} cannot be more than {MaxStringLength} characters";
            return null!;
        }

        public static string validateFloor(int floor, int numberOfFloors, PropertyType propertyType)
        {
            if (propertyType == PropertyType.Land || propertyType == PropertyType.Farm || propertyType == PropertyType.Garage)
            {
                if (floor != 0 || numberOfFloors != 0)
                {
                    return $"Floor and number of floors must be 0 for {propertyType}";
                }
            }

            if (propertyType == PropertyType.Apartment || propertyType == PropertyType.Condo || propertyType==PropertyType.Office)
            {
                if (floor <= -1)
                {
                    return $"Floor must be greater than -1 for {propertyType}";
                }
                if(numberOfFloors < 1)
                {
                    return $"Number of floors must be greater than 0 for {propertyType}";
                }

                if (floor > numberOfFloors)
                {
                    return $"Floor cannot be greater than number of floors for {propertyType}";
                }
            }

            if (propertyType == PropertyType.House || propertyType == PropertyType.Townhouse ||
                propertyType == PropertyType.Villa)
            {
                if (floor != 0)
                {
                    return $"Floor must be 0 for {propertyType}";
                }

                if (numberOfFloors < 1)
                {
                    return $"Number of floors must be greater than 0 for {propertyType}";
                }
            }

            return null!;

        }

        public static string ValidatePropertyType(PropertyType propertyType)
        {
            if (!Enum.IsDefined(typeof(PropertyType), propertyType))
            {
                return $"Property type {propertyType} is not valid";
            }

            return null!;
        }   
        

    }
}
