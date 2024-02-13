using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.UpdateListing
{
    public class UpdateListingCommandHandler : IRequestHandler<UpdateListingCommand, UpdateListingCommandResponse>
    {
        private readonly IListingRepository listingRepository;
        private readonly ICurrentUserService currentUserService;
        private readonly IPropertyRepository propertyRepository;

        public UpdateListingCommandHandler(IListingRepository listingRepository, ICurrentUserService currentUserService, IPropertyRepository propertyRepository)
        {
            this.listingRepository = listingRepository;
            this.currentUserService = currentUserService;
            this.propertyRepository = propertyRepository;
        }

        public async Task<UpdateListingCommandResponse> Handle(UpdateListingCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateListingCommandValidator(currentUserService, propertyRepository);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new UpdateListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                };
            }

            var listing = await listingRepository.FindByIdAsync(request.ListingId);

            if (!listing.IsSuccess)
            {
                return new UpdateListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { listing.Error }
                };
            }

            listing.Value.Update(request.Title, request.Price, request.Description, request.Photos, request.Negotiable);

            await listingRepository.UpdateAsync(listing.Value);

            return new UpdateListingCommandResponse
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
                    IsRent = listing.Value.IsRent,
                    Negotiable = listing.Value.Negotiable
                }
            };
        }
    }
}
