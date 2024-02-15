using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Queries.GetBasicInfoByIdProperty;

public class GetBasicInfoByIdPropertyQueryHandler(IPropertyRepository repository)
    : IRequestHandler<GetBasicInfoByIdPropertyQuery, GetBasicInfoByIdPropertyQueryResponse>
{
    public async Task<GetBasicInfoByIdPropertyQueryResponse> Handle(GetBasicInfoByIdPropertyQuery request, CancellationToken cancellationToken)
    {
        var property = await repository.FindByIdAsync(request.Id);
        if (property.IsSuccess)
        {
            return new GetBasicInfoByIdPropertyQueryResponse
            {
                Success = true,
                Property = new PropertyDto
                {
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
        
        return new GetBasicInfoByIdPropertyQueryResponse
        {
            Success = false,
            ValidationErrors = new List<string> { property.Error }
        };
    }
}