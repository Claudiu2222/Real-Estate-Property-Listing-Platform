using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetByIdListing
{
    public class GetByIdListingQueryResponse : BaseResponse
    {
        public ListingDto Listing { get; set; }
    }
}
