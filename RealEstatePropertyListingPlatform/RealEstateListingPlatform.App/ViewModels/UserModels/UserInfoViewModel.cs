using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels.UserModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

    }
}
