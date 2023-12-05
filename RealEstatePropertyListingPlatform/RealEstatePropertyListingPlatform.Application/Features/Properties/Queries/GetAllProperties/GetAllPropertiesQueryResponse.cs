
using RealEstatePropertyListingPlatform.Application.Responses;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryResponse : BaseResponse
    {
        public List<PropertyDto> Properties { get; set; }
        public bool WasFound { get; set; }
    }
}
