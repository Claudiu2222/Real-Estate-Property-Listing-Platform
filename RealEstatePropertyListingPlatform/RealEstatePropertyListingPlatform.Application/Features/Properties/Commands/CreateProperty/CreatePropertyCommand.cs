using MediatR;
using RealEstatePropertyListingPlatform.Domain.Enums;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<CreatePropertyCommandResponse>
    {
        public string StreetName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Country { get; set; } = default!;
        public PropertyType PropertyType { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int Floor { get; set; }
        public int NumberOfFloors { get; set; }
        public int SquareFeet { get; set; }
        public string Longitude { get; set; } = default!;
        public string Latitude { get; set; } = default!;

    }
}
