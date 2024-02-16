using Microsoft.AspNetCore.Components.Authorization;
using RealEstateListingPlatform.App.Contracts;
using RealEstateListingPlatform.App.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RealEstateListingPlatform.App.Auth
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationService authService;
        private readonly ITokenService tokenService;

        public CustomStateProvider(IAuthenticationService authService, ITokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var tokenString = await tokenService.GetTokenAsync();

                if(!string.IsNullOrEmpty(tokenString))
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(tokenString) as JwtSecurityToken;

                    if(jsonToken != null)
                    {
                        var userName = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name")?.Value ?? "user";
                        var userRoles = jsonToken?.Claims.Where(c => c.Type == "role")?.Select(c => c.Value) ?? Enumerable.Empty<string>();

                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, userName)
                            //maybe others
                        };

                        foreach (var role in userRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        identity = new ClaimsIdentity(claims, "Server authentication");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task Logout()
        {
            await authService.Logout();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(LoginViewModel loginParameters)
        {
            await authService.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Register(RegisterViewModel registerParameters)
        {
            await authService.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

    }
}
