using RealEstatePropertyListingPlatform.Application.Models.Identity;

namespace RealEstatePropertyListingPlatform.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<(int, List<UserModel>)> GetAll();
        Task<(int, UserModel)> GetCurrentUser();
        Task<(int, UserModel)> Update(UserModel userModel);
        Task<(int, string)> ChangePassword(ChangePasswordModel model);
        Task<(int, string)> ChangePasswordNotConnected(ChangePasswordNotConnectedModel model);
        Task<(int, string)> IsValidMail(string email);
        Task<(int, string)> Delete(string id);
        Task<(int, UserModel)> GetBasicInfo(string id);
        Task<(int, string)> Logout();


    }
}
