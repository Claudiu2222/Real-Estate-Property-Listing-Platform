using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandResponse:BaseResponse
    {

        public PropertyDto Property { get; set; }

    }
}
