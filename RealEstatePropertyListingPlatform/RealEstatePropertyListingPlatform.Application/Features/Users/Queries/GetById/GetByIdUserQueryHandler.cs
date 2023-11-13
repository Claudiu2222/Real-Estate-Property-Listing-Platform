using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDto>
    {
        private readonly IUserRepository repository;

        public GetByIdUserQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FindByIdAsync(request.Id);
            if (result.IsSuccess)
            {
                return new UserDto
                {
                    UserId = result.Value.UserId,
                    Email = result.Value.Email,
                    FirstName = result.Value.FirstName,
                    LastName = result.Value.LastName
                };
            }
            return new UserDto();
        }
    }
}
