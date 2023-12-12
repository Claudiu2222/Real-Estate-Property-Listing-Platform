using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdOwner
{
    public record GetByIdOwnerQuery() : IRequest<GetByIdOwnerQueryResponse>
    {
    }
}
