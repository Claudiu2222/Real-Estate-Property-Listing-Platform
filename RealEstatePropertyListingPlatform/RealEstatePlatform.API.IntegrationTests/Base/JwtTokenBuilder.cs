using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RealEstatePlatform.API.IntegrationTests.Base
{
    public class JwtTokenBuilder
    {
        public static Guid UserId = Guid.NewGuid();
        public List<Claim> Claims { get; } = new();
        public int ExpiresInMinutes { get; set; } = 30;

        private JwtTokenBuilder()
        {
        }
        public static JwtTokenBuilder Create()
        {
            return new JwtTokenBuilder();
        }
        public JwtTokenBuilder WithRole(string roleName)
        {
            Claims.Add(new Claim(ClaimTypes.Role, roleName));
            return this;
        }

        public JwtTokenBuilder WithUserName(string username)
        {
            Claims.Add(new Claim(ClaimTypes.Upn, username));
            return this;
        }

        public JwtTokenBuilder WithEmail(string email)
        {
            Claims.Add(new Claim(ClaimTypes.Email, email));
            return this;
        }

        public JwtTokenBuilder WithDepartment(string department)
        {
            Claims.Add(new Claim("department", department));
            return this;
        }

        public JwtTokenBuilder WithExpiration(int expiresInMinutes)
        {
            ExpiresInMinutes = expiresInMinutes;
            return this;
        }

        public JwtTokenBuilder WithNameIdentifier()
        {
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, UserId.ToString()));
            return this;
        }

        public string Build()
        {
            var token = new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                Claims,
                expires: DateTime.Now.AddMinutes(ExpiresInMinutes),
                signingCredentials: JwtTokenProvider.SigningCredentials
            );
            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
