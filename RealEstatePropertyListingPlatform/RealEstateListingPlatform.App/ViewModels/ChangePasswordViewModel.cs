using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RealEstateListingPlatform.App.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; } = string.Empty;
        [PasswordPropertyText]
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; } = string.Empty;
        [PasswordPropertyText]
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
