using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetAll
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
    {
        private readonly IUserRepository repository;

        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            GetAllUsersResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Success = true;
                response.Users = result.Value.Select(x => new UserDto
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                }).ToList();
                response.WasFound = response.Users.Count != 0;

                if (!response.WasFound)
                {
                    response.Message = "No users found";
                }

                return response;

            }


            response.ValidationErrors = new List<string> { result.Error };

            return response;
       
        }
    }
}
