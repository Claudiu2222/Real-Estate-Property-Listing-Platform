using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandResponse : BaseResponse
    {
        public ListingDto Listing { get; set; }
    }
}
