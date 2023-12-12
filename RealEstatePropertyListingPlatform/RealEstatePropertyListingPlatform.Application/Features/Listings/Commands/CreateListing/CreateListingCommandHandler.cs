using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;
using RealEstatePropertyListingPlatform.Domain.Entities;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.CreateListing
{
    public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, CreateListingCommandResponse>
    {
        private readonly IListingRepository listingRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IPropertyRepository propertyRepository;
        public CreateListingCommandHandler(IListingRepository listingRepository, ICurrentUserService currentUserService, IPropertyRepository propertyRepository)
        {
            this.listingRepository = listingRepository;
            this.currentUserService = currentUserService;
            this.propertyRepository = propertyRepository;
        }

        public async Task<CreateListingCommandResponse> Handle(CreateListingCommand request, CancellationToken cancellationToken)
        {
            var validatorProperty = new CreateListingCommandValidator(this.propertyRepository);

            var validationResult = await validatorProperty.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            Guid creatorId;
            try
            {
                creatorId = Guid.Parse(this.currentUserService.UserId);
            }
            catch (Exception)
            {
                return new CreateListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string>() { "Invalid user id." }
                };
            }


            var listing = Listing.Create(creatorId, request.PropertyId, request.Title, request.Price, request.Description, request.Photos, request.Negotiable);

            if (!listing.IsSuccess)
            {
                return new CreateListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string>() { listing.Error }
                };
            }

            await listingRepository.AddAsync(listing.Value);


            // the property was succesfully added

            return new CreateListingCommandResponse
            {
                Success = true,
                Listing = new ListingDto
                {
                    ListingId = listing.Value.ListingId,
                    ListingCreatorId = listing.Value.ListingCreatorId,
                    PropertyId = listing.Value.PropertyId,
                    Title = listing.Value.Title,
                    Price = listing.Value.Price,
                    Description = listing.Value.Description,
                    Photos = listing.Value.Photos,
                    DateCreated = listing.Value.DateCreated,
                    DateUpdated = listing.Value.DateUpdated,
                    Negotiable = listing.Value.Negotiable
                }
            
            };

        }
    }
    
}
