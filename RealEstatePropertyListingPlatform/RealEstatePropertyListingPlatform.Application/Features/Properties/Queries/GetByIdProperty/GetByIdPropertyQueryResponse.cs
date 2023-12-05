using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdProperty
{
    public class GetByIdPropertyQueryResponse : BaseResponse
    {

        public PropertyDto Property { get; set; }

    }
}
