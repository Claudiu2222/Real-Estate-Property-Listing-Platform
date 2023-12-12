using MediatR;

namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries.GetPagedUsers
{
    public class GetPagedUsersQuery : IRequest<GetPagedUsersResponse>
    {

        public GetPagedUsersQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }


    }
}
