using RealEstatePropertyListingPlatform.Application.Models.Identity;

namespace RealEstatePropertyListingPlatform.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<(int, List<UserModel>)> GetAll();
        Task<(int, string)> Delete(string id);
        Task<(int, string)> Logout();

    }
}
