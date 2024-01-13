using RealEstateListingPlatform.App.ViewModels.Validation;
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
        [Required(ErrorMessage = "Street name is required")]
        public string StreetName { get; set; } = default!;

        [JsonPropertyName("city")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [JsonPropertyName("region")]
        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set; } = default!;

        [JsonPropertyName("postalCode")]
        [Required(ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; } = default!;

        [JsonPropertyName("country")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = default!;

        [JsonPropertyName("propertyType")]
        [Required(ErrorMessage = "Property type is required")]
        public PropertyType PropertyType { get; set; }

        [JsonPropertyName("numberOfRooms")]
        [Required(ErrorMessage = "Number of rooms is required")]
        [ValidateInteger(0, 20)]
        public int NumberOfRooms { get; set; }

        [JsonPropertyName("numberOfBathrooms")]
        [Required(ErrorMessage = "Number of bathrooms is required")]
        [ValidateInteger(0, 20)]
        public int NumberOfBathrooms { get; set; }

        [JsonPropertyName("floor")]
        [Required(ErrorMessage = "Floor is required")]
        [ValidateInteger(0, 100)]
        public int Floor { get; set; }

        [JsonPropertyName("numberOfFloors")]
        [Required(ErrorMessage = "Number of floors is required")]
        [ValidateInteger(0, 100)]
        public int NumberOfFloors { get; set; }

        [JsonPropertyName("squareFeet")]
        [Required(ErrorMessage = "Square feet is required")]
        [ValidateInteger(0, 1_000)]
        public int SquareFeet { get; set; }
    }
}
