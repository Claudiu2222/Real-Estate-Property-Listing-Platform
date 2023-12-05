using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdProperty
{
    public record GetByIdPropertyQuery (Guid Id): IRequest<GetByIdPropertyQueryResponse>
    {
    }
}
