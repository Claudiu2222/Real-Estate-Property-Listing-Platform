using MediatR;
using RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetByIdUser;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetById
{
    public record GetByIdUserQuery(Guid Id) : IRequest<GetByIdUserQueryResponse>
    {

    }
}
