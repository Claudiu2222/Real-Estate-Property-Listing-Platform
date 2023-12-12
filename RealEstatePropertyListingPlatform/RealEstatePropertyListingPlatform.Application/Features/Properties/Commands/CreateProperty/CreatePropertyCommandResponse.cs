using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandResponse : BaseResponse
    {
        public CreatePropertyCommandResponse() : base()
        {

        }

        public PropertyDto Property { get; set; }
    }
}
