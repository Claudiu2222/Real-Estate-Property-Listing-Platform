using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetPagedUsers
{
    public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, GetPagedUsersResponse>
    {

        private readonly IUserRepository userRepository;
        public GetPagedUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

        }

        public async Task<GetPagedUsersResponse> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
        {

            var totalCount = await userRepository.GetCountAsync();

            if (totalCount.Value[0] < (request.PageNumber - 1) * request.PageSize)
            {
                return new GetPagedUsersResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Page number out of range" },
                    TotalCount = totalCount.Value[0],
                };
            }
            var result = await userRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);

            
           
            return new GetPagedUsersResponse
                {
                    Success = true,
                    Users = result.Value.Select(x => new UserDto
                    {
                        UserId = x.UserId,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        PhoneNumber = x.PhoneNumber
                    }).ToList(),
                    TotalCount = totalCount.Value[0],
                };
            

        }
    }
    }

