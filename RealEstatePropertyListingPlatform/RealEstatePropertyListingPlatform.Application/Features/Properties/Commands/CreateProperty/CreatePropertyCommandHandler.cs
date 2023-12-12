using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;


namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly ICurrentUserService currentUserService;
        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, ICurrentUserService currentUserService)
        {
            this.propertyRepository = propertyRepository;
            this.currentUserService = currentUserService;
        }

        public async Task<CreatePropertyCommandResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            //var validatorProperty = new CreatePropertyCommandValidator(this.currentUserService);
            var validatorProperty = new CreatePropertyCommandValidator();

            var validationResult = await validatorProperty.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            Guid ownerId;
            try
            { 
               ownerId = Guid.Parse(currentUserService.UserId);
            }
            catch (Exception)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string>() { "The user id is not valid." }
                };
            }


            var property = Property.Create(ownerId, request.StreetName, request.City, request.Region, request.PostalCode,
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

            await propertyRepository.AddAsync(property.Value);


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
