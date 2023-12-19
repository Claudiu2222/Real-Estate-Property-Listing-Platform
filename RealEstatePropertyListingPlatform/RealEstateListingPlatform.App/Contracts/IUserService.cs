using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using RealEstateListingPlatform.App.ViewModels.UserModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IUserService
    {

        Task CreateAdmin(RegisterViewModel createAdminRequest);
        Task<ApiResponseSingleData<UserInfoViewModel>> GetInfoCurrentUser();
        Task<ApiResponseSingleData<UserInfoViewModel>> Update(UserInfoViewModel updateRequest);
        Task<ApiResponseSingleData<UserInfoViewModel>> GetInfo(string id);

    }
}
