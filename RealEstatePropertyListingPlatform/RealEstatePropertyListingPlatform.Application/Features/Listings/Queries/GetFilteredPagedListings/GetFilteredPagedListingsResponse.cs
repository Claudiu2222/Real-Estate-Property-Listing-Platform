using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetFilteredPagedListings
{
    public class GetFilteredPagedListingsResponse : BaseResponse
    {
        public List<ListingDto> Listings { get; set; }
        public int TotalCount { get; set; }
    }
}
