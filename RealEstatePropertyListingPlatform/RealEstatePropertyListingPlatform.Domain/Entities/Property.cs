using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
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

        private Property(){}

        public static Result<Property> Create(Guid ownerId, string streetName, string city, string region, string postalCode,
            string country, PropertyType propertyType, int numberOfRooms, int numberOfBathrooms, int floor, int numberOfFloors,int squareFeet)
        {

            string error = PropertyValidator.ValidateProperty(streetName, city, region, postalCode, country,
                               propertyType, numberOfRooms, numberOfBathrooms, floor,numberOfFloors, squareFeet);

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


    }   



    }



    
           