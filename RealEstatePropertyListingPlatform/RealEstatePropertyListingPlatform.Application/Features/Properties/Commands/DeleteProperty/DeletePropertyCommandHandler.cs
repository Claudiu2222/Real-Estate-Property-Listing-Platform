using System.Transactions;
using MediatR;
using RealEstatePropertyListingPlatform.Application.Persistence;

namespace RealEstatePropertyListingPlatform.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, DeletePropertyCommandResponse>
    {
        
        private readonly IPropertyRepository propertyRepository;

        public DeletePropertyCommandHandler( IPropertyRepository propertyRepository, IListingRepository listingRepository)
        {
            
            this.propertyRepository = propertyRepository;
        }

        public async Task<DeletePropertyCommandResponse> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            var result = await this.propertyRepository.DeleteAsync(request.PropertyId);

            if (!result.IsSuccess)
            {
                return new DeletePropertyCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };
            }

            return new DeletePropertyCommandResponse
            {
                Success = true
            };

        }
    }
}
