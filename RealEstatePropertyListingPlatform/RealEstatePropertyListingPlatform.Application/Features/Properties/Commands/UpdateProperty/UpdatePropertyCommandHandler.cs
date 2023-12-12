using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, UpdatePropertyCommandResponse>
    {
        
        private readonly IPropertyRepository propertyRepository;
        private readonly ICurrentUserService currentUserService;

        public UpdatePropertyCommandHandler( IPropertyRepository propertyRepository, ICurrentUserService currentUserService)
        {
            
            this.propertyRepository = propertyRepository;
            this.currentUserService = currentUserService;
        }

        public async Task<UpdatePropertyCommandResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {

            var validator = new UpdatePropertyCommandValidator(currentUserService, propertyRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }


            var property = await propertyRepository.FindByIdAsync(request.PropertyId);

            if (!property.IsSuccess)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { property.Error }
                };
            }

            property.Value.Update(request.StreetName, request.City, request.Region, request.PostalCode,
                                    request.Country, request.PropertyType, request.NumberOfRooms, request.NumberOfBathrooms,
                                    request.Floor, request.NumberOfFloors, request.SquareFeet);


            await propertyRepository.UpdateAsync(property.Value);



            return new UpdatePropertyCommandResponse
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
                    SquareFeet = property.Value.SquareFeet

                }
            };

        }
    }
}
