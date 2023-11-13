namespace RealEstatePropertyListingPlatform.Application.Features.Users
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;


    }
}
