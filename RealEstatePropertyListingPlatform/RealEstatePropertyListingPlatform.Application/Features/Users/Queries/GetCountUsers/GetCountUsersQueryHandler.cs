using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetCountUsers
{
    public class GetCountUsersQueryHandler :  IRequestHandler<GetCountUsersQuery, int>
    {

        private readonly IUserRepository userRepository;

        public GetCountUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> Handle(GetCountUsersQuery request, CancellationToken cancellationToken)
        {
           var result = await userRepository.GetCountAsync();
           return result.Value[0];
        }
    }
}
