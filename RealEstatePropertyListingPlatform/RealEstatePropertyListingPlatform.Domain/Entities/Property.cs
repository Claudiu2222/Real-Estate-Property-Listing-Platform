using RealEstatePropertyListingPlatform.Domain.ClassValidators;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class Property
    {
        public Guid PropertyId { get; private set; }
        public Guid OwnerId { get; private set; }
        public string StreetName { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }
        public PropertyType PropertyType { get; private set; }
        public int NumberOfRooms { get; private set; }
        public int NumberOfBathrooms { get; private set; }
        public int Floor { get; private set; }
        public int NumberOfFloors { get; private set; }
        public int SquareFeet { get; private set; }
        private Property() { }

        public static Result<Property> CreateBase(Guid ownerId, string streetName, string city, string region, string postalCode,
            string country, PropertyType propertyType, int numberOfRooms, int numberOfBathrooms, int floor, int numberOfFloors, int squareFeet)
        {

            string error = PropertyValidator.ValidateProperty(streetName, city, region, postalCode, country,
                               propertyType, numberOfRooms, numberOfBathrooms, floor, numberOfFloors, squareFeet);

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Property>.Failure(error);
            }

            var property = new Property
            {
                PropertyId = Guid.NewGuid(),
                OwnerId = ownerId,
                StreetName = streetName,
                City = city,
                Region = region,
                PostalCode = postalCode,
                Country = country,
                PropertyType = propertyType,
                NumberOfRooms = numberOfRooms,
                NumberOfBathrooms = numberOfBathrooms,
                Floor = floor,
                NumberOfFloors = numberOfFloors,
                SquareFeet = squareFeet
            };

            return Result<Property>.Success(property);
        }

        public Result<Property> UpdateStreetName(string streetName)
        {
            string error = PropertyValidator.ValidateString(streetName, nameof(streetName));

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Property>.Failure(error);
            }

            StreetName = streetName;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateCity(string city)
        {
            string error = PropertyValidator.ValidateString(city, nameof(city));

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Property>.Failure(error);
            }

            City = city;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateRegion(string region)
        {
            string error = PropertyValidator.ValidateString(region, nameof(region));

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Property>.Failure(error);
            }

            Region = region;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdatePostalCode(string postalCode)
        {
            string error = PropertyValidator.ValidateString(postalCode, nameof(postalCode));

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Property>.Failure(error);
            }

            PostalCode = postalCode;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateCountry(string country)
        {
            string error = PropertyValidator.ValidateString(country, nameof(country));

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<Property>.Failure(error);
            }

            Country = country;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdatePropertyType(PropertyType propertyType)
        {
            string error = PropertyValidator.ValidatePropertyType(propertyType);
            if (string.IsNullOrWhiteSpace(error))
            {
                Result<Property>.Failure(error);
            }
            PropertyType = propertyType;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateNumberOfRooms(int numberOfRooms)
        {
            string error = PropertyValidator.ValidateRooms(numberOfRooms, PropertyType);
            if (string.IsNullOrWhiteSpace(error))
            {
                Result<Property>.Failure(error);
            }
            NumberOfRooms = numberOfRooms;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateNumberOfBathrooms(int numberOfBathrooms)
        {
            string error = PropertyValidator.ValidateBathrooms(numberOfBathrooms, PropertyType);
            if (string.IsNullOrWhiteSpace(error))
            {
                Result<Property>.Failure(error);
            }
            NumberOfBathrooms = numberOfBathrooms;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateFloor(int floor)
        {
            string error = PropertyValidator.validateFloor(floor, NumberOfFloors, PropertyType);
            if (string.IsNullOrWhiteSpace(error))
            {
                Result<Property>.Failure(error);
            }
            Floor = floor;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateNumberOfFloors(int numberOfFloors)
        {
            string error = PropertyValidator.validateFloor(Floor, numberOfFloors, PropertyType);
            if (string.IsNullOrWhiteSpace(error))
            {
                Result<Property>.Failure(error);
            }
            NumberOfFloors = numberOfFloors;

            return Result<Property>.Success(this);
        }

        public Result<Property> UpdateSquareFeet(int squareFeet)
        {
            string error = PropertyValidator.ValidateSquareFeet(squareFeet);
            if (string.IsNullOrWhiteSpace(error))
            {
                Result<Property>.Failure(error);
            }
            SquareFeet = squareFeet;

            return Result<Property>.Success(this);
        }


    }



}




