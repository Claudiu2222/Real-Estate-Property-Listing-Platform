using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IValidationCodeService
    {
        public Task<ApiResponseValidationCode> SendValidationCodeAsync(ValidationCodeViewModel email);
        public Task<ApiResponseValidationCode> ValidateValidationCodeAsync(ValidateValidationCodeViewModel validateValidationCodeViewModel);
        public Task<ApiResponseValidationCode> DeleteValidationCodeAsync(ValidationCodeViewModel email);
    }
}
