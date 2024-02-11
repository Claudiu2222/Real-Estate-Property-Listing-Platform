namespace RealEstateListingPlatform.App.ViewModels
{


    public class ListingViewModel
    {
        public string ListingId { get; set; }
        public string ListingCreatorId { get; set; }
        public string PropertyId { get; set; }
        public string? Title { get; set; }
        public PriceViewModel? Price { get; set; }
        public string? Description { get; set; }
        public List<string>? Photos { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsRent { get; set; }
        public bool Negotiable { get; set; }
    }

 }
