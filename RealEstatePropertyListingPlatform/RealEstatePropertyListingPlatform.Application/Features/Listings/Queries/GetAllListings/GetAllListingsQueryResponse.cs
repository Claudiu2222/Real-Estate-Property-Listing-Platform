using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetAllListings
{
    public class GetAllListingsQueryResponse : BaseResponse
    {
        public List<ListingDto> Listings { get; set; }
        public bool WasFound { get; set; }
    }
}
