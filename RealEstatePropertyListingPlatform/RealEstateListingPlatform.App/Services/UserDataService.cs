using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.Services.Responses;
using RealEstateListingPlatform.App.ViewModels;
using RealEstateListingPlatform.App.ViewModels.UserModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RealEstateListingPlatform.App.Services
{
    //primary constructor
    public class UserDataService(HttpClient httpClient, ITokenService tokenService) : IUserService
    {
        private const string requestUriAuth = "api/v1/authentication";
        private const string requestUriUser = "api/v1/users";
        public async Task CreateAdmin (RegisterViewModel createAdminRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PostAsJsonAsync($"{requestUriAuth}/register/admin", createAdminRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }

        public async Task<ApiResponseSingleData<UserInfoViewModel>> GetInfoCurrentUser()
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.GetAsync($"{requestUriUser}/currentuser");

            if (!result.IsSuccessStatusCode)//if no succes code recivied from backend
            {
                return new ApiResponseSingleData<UserInfoViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = await result.Content.ReadAsStringAsync()
                };
            }

            //the result is successfull
            var response = await result.Content.ReadFromJsonAsync<UserInfoViewModel>();

            //response.Id = " "; //shouldn't have acces at the id
            return new ApiResponseSingleData<UserInfoViewModel>
            {
                IsSuccess = true,
                Data = response,
                Message = "User info retrieved successfully"
            };


        }

        public async Task<ApiResponseSingleData<UserInfoViewModel>> Update(UserInfoViewModel updateRequest)
        {

            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

            var result = await httpClient.PutAsJsonAsync($"{requestUriUser}", updateRequest);

            if (!result.IsSuccessStatusCode)//if no succes code recivied from backend
            {
                return new ApiResponseSingleData<UserInfoViewModel>
                {
                    IsSuccess = false,
                    ValidationErrors = "User couldn't be updated (probably username/email already exists)"
                };
            }

            //the result is successfull
            var response = await result.Content.ReadFromJsonAsync<UserInfoViewModel>();
            return new ApiResponseSingleData<UserInfoViewModel>
            {
                IsSuccess = true,
                Data = response,
                Message = "User updated successfully"
            };

        }


    }
}
