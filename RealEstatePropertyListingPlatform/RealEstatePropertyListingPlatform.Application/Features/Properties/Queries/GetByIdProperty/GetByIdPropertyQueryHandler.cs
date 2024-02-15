using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetByIdProperty
{
    public class GetByIdPropertyQueryHandler : IRequestHandler<GetByIdPropertyQuery, GetByIdPropertyQueryResponse>
    {
        private readonly IPropertyRepository propertyRepository;

        public GetByIdPropertyQueryHandler( IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<GetByIdPropertyQueryResponse> Handle(GetByIdPropertyQuery request, CancellationToken cancellationToken)
        {
            var property = await propertyRepository.FindByIdAsync(request.Id);
            
            if (property.IsSuccess)
            {   

                return new GetByIdPropertyQueryResponse
                {
                    Success = true,
                    Property = new PropertyDto
                    {
                        PropertyId = property.Value.PropertyId,
                        OwnerId = property.Value.OwnerId,
                        StreetName = property.Value.StreetName,
                        City = property.Value.City,
                        Region = property.Value.Region,
                        Country = property.Value.Country,
                        PostalCode = property.Value.PostalCode,
                        PropertyType = property.Value.PropertyType,
                        NumberOfRooms = property.Value.NumberOfRooms,
                        NumberOfBathrooms = property.Value.NumberOfBathrooms,
                        Floor = property.Value.Floor,
                        NumberOfFloors = property.Value.NumberOfFloors,
                        Longitude = property.Value.Longitude,
                        Latitude = property.Value.Latitude,
                        SquareFeet = property.Value.SquareFeet
                    }
                };
            }

            return new GetByIdPropertyQueryResponse
            {
                Success = false,
                ValidationErrors = new List<string> { property.Error }
            };
            

        }
    }
}
