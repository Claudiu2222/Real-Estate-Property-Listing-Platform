using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, GetAllPropertiesQueryResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository)
        {
            this.propertyRepository = propertyRepository;
        }

        public async Task<GetAllPropertiesQueryResponse> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            GetAllPropertiesQueryResponse response = new();

            var result = await propertyRepository.GetAllAsync();

            if (result.IsSuccess)
            {

                response.Success = true;

                response.Properties = result.Value.Select(x => new PropertyDto
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
                }).ToList();

                response.WasFound = response.Properties.Count != 0;

                if (!response.WasFound)
                {
                    response.Message = "No properties found";
                }

                return response;
            }

            
            response.ValidationErrors = new List<string> { result.Error };

            return response;

        }

    }
}
