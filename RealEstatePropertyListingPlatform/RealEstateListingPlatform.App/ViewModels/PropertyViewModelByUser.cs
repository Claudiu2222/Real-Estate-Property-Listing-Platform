using RealEstatePropertyListingPlatform.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class PropertyViewModelByUser
    {
        [JsonPropertyName("propertyId")]
        public Guid PropertyId { get; set; }

        [JsonPropertyName("ownerId")]
        public Guid OwnerId { get; set; }

        [JsonPropertyName("streetName")]
        public string StreetName { get; set; } = default!;

        [JsonPropertyName("city")]
        public string City { get; set; } = default!;

        [JsonPropertyName("region")]
        public string Region { get; set; } = default!;

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; } = default!;

        [JsonPropertyName("country")]
        public string Country { get; set; } = default!;

        [JsonPropertyName("propertyType")]
        public int PropertyType { get; set; }

        [JsonPropertyName("numberOfRooms")]
        public int NumberOfRooms { get; set; }

        [JsonPropertyName("numberOfBathrooms")]
        public int NumberOfBathrooms { get; set; }

        [JsonPropertyName("floor")]
        public int Floor { get; set; }

        [JsonPropertyName("numberOfFloors")]
        public int NumberOfFloors { get; set; }

        [JsonPropertyName("squareFeet")]
        public int SquareFeet { get; set; }
    }
}
