using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetBasicInfoByIdProperty
{
    
    public record GetBasicInfoByIdPropertyQuery(Guid Id): IRequest<GetBasicInfoByIdPropertyQueryResponse>
    {
    }
}