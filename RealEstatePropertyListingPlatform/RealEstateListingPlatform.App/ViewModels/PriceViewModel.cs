using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class PriceViewModel
    {
        [Required(ErrorMessage = "Value is required")]
        [Range(0, 1_000_000_000)]
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}
