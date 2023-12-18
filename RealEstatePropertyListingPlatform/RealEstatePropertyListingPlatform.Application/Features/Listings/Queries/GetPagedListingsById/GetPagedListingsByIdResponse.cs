using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Queries.GetPagedListingsById
{
    public class GetPagedListingsByIdResponse : BaseResponse
    {
        public List<ListingDto> Listings { get; set; }
        public int TotalCount { get; set; }
    }
}
