using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

    }
}
