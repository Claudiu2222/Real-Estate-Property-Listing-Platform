using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    //primary constructor
    public class UserDataService(HttpClient httpClient, ITokenService tokenService) : IUserService
    {
        public async Task CreateAdmin (RegisterViewModel createAdminRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register/admin", createAdminRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }
    }
}
