using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        [MinLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
        public string ValidationCode { get; set; } = string.Empty;

    }
}
