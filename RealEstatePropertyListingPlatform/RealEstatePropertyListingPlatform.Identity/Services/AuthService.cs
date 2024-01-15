using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstatePropertyListingPlatform.Application.Contracts.Identity;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Application.Models.Identity;
using RealEstatePropertyListingPlatform.Identity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstatePropertyListingPlatform.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICurrentUserService currentUserService;
        private readonly IConfiguration configuration;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ICurrentUserService currentUserService ,
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.currentUserService = currentUserService;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {

            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return (0, "User already exists");

            var emailExists = await userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return (0, "Email already exists");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber
            };

            var psswdValidators = new PasswordValidator<ApplicationUser>();

            var psswdValidationResult = await psswdValidators.ValidateAsync(userManager, user, model.Password);
            if (!psswdValidationResult.Succeeded)
            {
                return (0, $"Password is not strong enough. (it needs to contain at lease 1 Uppercaseletter, 1 number and 1 symbol)");
            }

            var createUserResult = await userManager.CreateAsync(user, model.Password);

            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            if (await roleManager.RoleExistsAsync(UserRole.User))
                await userManager.AddToRoleAsync(user, role);

            return (1, $"{role} created successfully!");
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username!);
            
            if (user == null)
                return (0, "Invalid username");
            if (!await userManager.CheckPasswordAsync(user, model.Password!))
                return (0, "Invalid password");

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               //new Claim(ClaimTypes.Role, string.Join(",", userRoles))

            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }

        public async Task<(int, List<UserModel>)> GetAll()
        {
            var users = this.userManager.Users.ToList();

            var usersModel = users.Select(u => new UserModel
            {
                Id = u.Id,
                Email = u.Email,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
                Username = u.UserName
            }).ToList();

            return (1, usersModel);

        }

        public async Task<(int, UserModel)> GetCurrentUser()
        {
            var user = await this.userManager.FindByIdAsync(this.currentUserService.UserId);

            if (user == null)
            {
                return (404, null!);
            }

            var userModel = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName
            };

            return (1, userModel);
        }
        
        public async Task<(int, UserModel)> GetBasicInfo(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                return (404, null!);
            }

            var userModel = new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            return (1, userModel);
        }


        public async Task<(int, UserModel)> Update(UserModel userModel)
        {

            var user = await this.userManager.FindByIdAsync(this.currentUserService.UserId);

            if (user == null)
            {
                return (404, null!);
            }

            var userExists = await userManager.FindByNameAsync(userModel.Username);
            
            if (userExists != null && userExists.Id != this.currentUserService.UserId) //if exists in db a diff user from the current one with the same name
            {
                return (0, null!);
            }

            var emailExists = await userManager.FindByEmailAsync(userModel.Email);

            if (emailExists != null && emailExists.Id != this.currentUserService.UserId)
                return (0, null!);


            user.Name = userModel.Name;
            user.Email = userModel.Email;
            user.PhoneNumber = userModel.PhoneNumber;
            user.UserName = userModel.Username;


            var result = await this.userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return (0, null!);
            }

            return (1, userModel);
            
        }

        public async Task<(int, string)> Delete(string id)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                return (0, "Invalid id");
            }

            if (id == this.currentUserService.UserId)
            {
                return (0, "You can't delete yourself");
            }


            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return (404, "User not found");
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return (1, "User deleted successfully");
            }

            return (0, "User couldn't be deleted");

        }

        public async Task<(int, string)> ChangePassword(ChangePasswordModel model)
        {
            var user = await this.userManager.FindByIdAsync(this.currentUserService.UserId);

            if (user == null)
            {
                return (404, "User not found");
            }

            var result = await this.userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                return (1, "Password changed successfully");
            }

            //password validation errors
            var errors = result.Errors.Select(e => e.Description).ToList();
            if (errors.Count > 0)
            {
                return (0, string.Join("", errors));
            }

            return (0, "Password couldn't be changed");
        }

        public async Task<(int, string)> ChangePasswordNotConnected(ChangePasswordNotConnectedModel model)
        {
            var user =  await this.userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return (404, "Email not found");
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            var result = await this.userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                return (1, "Password changed successfully");
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            if (errors.Count > 0)
            {
                return (0, string.Join("", errors));
            }

            return (0, "Password couldn't be changed");
        }

        public async Task<(int, string)> IsValidMail(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return (404, "Email not found");
            }

            return (1, "Email found");
        }

        public async Task<(int, string)> Logout()
        {
            await signInManager.SignOutAsync();
            return (1, "User logged out successfully!");
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWT:ValidIssuer"]!,
                Audience = configuration["JWT:ValidAudience"]!,
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
