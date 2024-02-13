using System.Net;
using Google;
using MediatR;
using RealEstatePropertyListingPlatform.Application.Contracts;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Listings.Commands.DeleteListing
{
    public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, DeleteListingCommandResponse>
    {
        private readonly IListingRepository listingRepository;
        private readonly IImageStorageService imageStorageService;

        public DeleteListingCommandHandler(IListingRepository listingRepository, IImageStorageService imageStorageService)
        {
            this.listingRepository = listingRepository;
            this.imageStorageService = imageStorageService;
        }

        public async Task<DeleteListingCommandResponse> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
        {

            var listing = await listingRepository.FindByIdAsync(request.ListingId);
            if (listing == null)
            {
                return new DeleteListingCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Listing not found." }
                
            };
            }

           
            foreach (var photo in listing.Value.Photos)
            {
                try
                {
                    await imageStorageService.DeleteImageAsync(photo.Split('/').Last().Split('?').First());
                }
                catch (GoogleApiException ex)
                {
                    if (ex.HttpStatusCode != HttpStatusCode.NotFound)
                    {
                        return new DeleteListingCommandResponse
                        {
                            Success = false,
                            ValidationErrors = new List<string> { ex.Message }
                        };
                    }
                }
            }
            
           

            var result = await listingRepository.DeleteAsync(request.ListingId);

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
