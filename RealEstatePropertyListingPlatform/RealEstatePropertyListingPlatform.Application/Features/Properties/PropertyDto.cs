using RealEstatePropertyListingPlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties
{
    public class PropertyDto
    {
        public Guid PropertyId { get; set; }
        public Guid OwnerId { get;  set; }
        public string StreetName { get;  set; } = default!;
        public string City { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Country { get; set; } = default!;
        public PropertyType PropertyType { get;  set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int Floor { get; set; }
        public int NumberOfFloors { get; set; }
        public int SquareFeet { get; set; }
    }
}
