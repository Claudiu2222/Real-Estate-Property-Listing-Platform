namespace RealEstatePropertyListingPlatform.Application.Models.Identity
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
