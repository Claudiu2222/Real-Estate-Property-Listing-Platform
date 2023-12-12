using RealEstateListingPlatform.App.ViewModels;

namespace RealEstateListingPlatform.App.Contracts
{
    public interface IUserService
    {

        Task CreateAdmin(RegisterViewModel createAdminRequest);


    }
}
