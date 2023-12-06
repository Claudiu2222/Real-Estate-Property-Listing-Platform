using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommandResponse : BaseResponse
    {
        public CreateListingCommandResponse() : base()
        {

        }

        public ListingDto Listing { get; set; }
    }
}
