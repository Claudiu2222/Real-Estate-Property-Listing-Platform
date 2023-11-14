using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetById
{
    public record GetByIdUserQuery(Guid Id) : IRequest<UserDto>
    {

    }
}
