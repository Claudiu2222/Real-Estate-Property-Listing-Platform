using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListings
{
    public class GetPagedListingsResponse : BaseResponse
    {
        public List<ListingDto> Listings { get; set; }
        public int TotalCount { get; set; }
    }
}
