using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{

    public class ListingViewModelCreate
    {
        public string PropertyId { get; set; }
        [Required (ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        public PriceViewModel? Price { get; set; }
        [Required (ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public List<string>? Photos { get; set; }
        public bool IsRent { get; set; }
        [Required (ErrorMessage = "Negotiable is required")]
        public bool Negotiable { get; set; }
    }
}
