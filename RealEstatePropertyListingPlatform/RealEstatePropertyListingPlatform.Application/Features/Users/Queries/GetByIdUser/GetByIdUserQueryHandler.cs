using MediatR;
using RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetByIdUser;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse>
    {
        private readonly IUserRepository repository;

        public GetByIdUserQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.FindByIdAsync(request.Id);

            if (result.IsSuccess)
            {
                return new GetByIdUserQueryResponse
                {
                    Success = true,
                    User = new UserDto
                    {
                        UserId = result.Value.UserId,
                        Email = result.Value.Email,
                        FirstName = result.Value.FirstName,
                        LastName = result.Value.LastName,
                        PhoneNumber = result.Value.PhoneNumber
                    }
                };
            }

            return new GetByIdUserQueryResponse
            {
                Success = false,
                ValidationErrors = new List<string> { result.Error }
            };
        }
    }
}
