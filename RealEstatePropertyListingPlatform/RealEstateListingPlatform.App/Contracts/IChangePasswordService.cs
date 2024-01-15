using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IChangePasswordService
    {
        Task<ApiResponseChangePassword> ChangePasswordAsync(ChangePasswordViewModel changePasswordViewModel);
        Task<ApiResponseChangePassword> ChangePasswordNoConnectionAsync(ChangePasswordNoConnectionViewModel changePasswordViewModel);
        Task<ApiResponseChangePassword> IsValidMail(IsValidMailViewModel isValidMailViewModel);
    }
}
