using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdOwner
{
    public class GetByIdOwnerQueryHandler : IRequestHandler<GetByIdOwnerQuery, GetByIdOwnerQueryResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ICurrentUserService currentUserService;

        public GetByIdOwnerQueryHandler( IPropertyRepository propertyRepository, ICurrentUserService currentUserService)
        {
            this.propertyRepository = propertyRepository;
            this.currentUserService = currentUserService;
        }

        public async Task<GetByIdOwnerQueryResponse> Handle(GetByIdOwnerQuery request, CancellationToken cancellationToken)
        {
            var currentUserIdClaim = currentUserService.UserId;
            Guid currentUserId;

            try
            {
                currentUserId = Guid.Parse(currentUserIdClaim);
            }
            catch (Exception)
            {
                return new GetByIdOwnerQueryResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "The user id is not valid." }
                };
            }

            var property = await propertyRepository.FindByIdOwnerAsync(currentUserId);

            
            if(property.IsSuccess)
            {
                return new GetByIdOwnerQueryResponse
                {
                    Success = true,
                    Properties = property.Value.Select(x => new PropertyDto
                    {
                        PropertyId = x.PropertyId,
                        OwnerId = x.OwnerId,
                        StreetName = x.StreetName,
                        City = x.City,
                        Region = x.Region,
                        Country = x.Country,
                        PostalCode = x.PostalCode,
                        PropertyType = x.PropertyType,
                        NumberOfRooms = x.NumberOfRooms,
                        NumberOfBathrooms = x.NumberOfBathrooms,
                        Floor = x.Floor,
                        NumberOfFloors = x.NumberOfFloors,
                        SquareFeet = x.SquareFeet
                    }).ToList()
                };
            }

            return new GetByIdOwnerQueryResponse
            {
                Success = false,
                ValidationErrors = new List<string> { property.Error }
            };
            

        }
    }
}
