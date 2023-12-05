using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IUserRepository userRepository;
        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUserRepository userRepository)
        {
            this.propertyRepository = propertyRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreatePropertyCommandResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validatorProperty = new CreatePropertyCommandValidator(this.userRepository);

            var validationResult = await validatorProperty.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var property = Property.Create(request.OwnerId, request.StreetName, request.City, request.Region, request.PostalCode,
                                           request.Country, request.PropertyType, request.NumberOfRooms, request.NumberOfBathrooms,
                                           request.Floor, request.NumberOfFloors, request.SquareFeet);

            if (!property.IsSuccess)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string>() { property.Error }
                };
            }

            try
            {
                await propertyRepository.AddAsync(property.Value);
            }
            catch (Exception ex)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string>() { ex.Message }
                };
            }


            // the property was succesfully added

            return new CreatePropertyCommandResponse
            {
                Success = true,
                Property = new PropertyDto
                {
                    PropertyId = property.Value.PropertyId,
                    OwnerId = property.Value.OwnerId,
                    StreetName = property.Value.StreetName,
                    City = property.Value.City,
                    Region = property.Value.Region,
                    PostalCode = property.Value.PostalCode,
                    Country = property.Value.Country,
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
