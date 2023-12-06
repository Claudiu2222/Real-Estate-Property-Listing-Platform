using MediatR;
using RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.DeleteProperty;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.DeleteListing
{
    public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, DeleteListingCommandResponse>
    {
        private readonly IListingRepository listingRepository;

        public DeleteListingCommandHandler(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        public async Task<DeleteListingCommandResponse> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
        {
            var result = await this.listingRepository.DeleteAsync(request.ListingId);

            if (!result.IsSuccess)
            {
                return new DeleteListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };
            }

            return new DeleteListingCommandResponse
            {
                Success = true
            };
        }

    }
}
