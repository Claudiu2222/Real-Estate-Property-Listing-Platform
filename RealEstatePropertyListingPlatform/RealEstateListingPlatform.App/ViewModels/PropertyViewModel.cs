using RealEstatePropertyListingPlatform.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class PropertyViewModel
    {
        [Required(ErrorMessage = "Street Name is required")]
        public string StreetName { get; set; } = default!;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Region is required")]
        public string Region { get; set; } = default!;

        [Required(ErrorMessage = "Postal Code is required")]
        public string PostalCode { get; set; } = default!;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = default!;

        [Required(ErrorMessage = "Property Type is required")]
        public PropertyType PropertyType { get; set; }

        [Required(ErrorMessage = "Number of Rooms is required")]
        [Range(0, 20)]
        public int NumberOfRooms { get; set; }

        [Required(ErrorMessage = "Number of Bathrooms is required")]
        [Range(0, 20)]
        public int NumberOfBathrooms { get; set; }

        [Required(ErrorMessage = "Floor is required")]
        [Range(0, 100) ]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Number of Floors is required")]
        [Range(0, 100) ]
        public int NumberOfFloors { get; set; }

        [Required(ErrorMessage = "Square Feet is required")]
        [Range(0, 10_000)]
        public int SquareFeet { get; set; }

    }
}
