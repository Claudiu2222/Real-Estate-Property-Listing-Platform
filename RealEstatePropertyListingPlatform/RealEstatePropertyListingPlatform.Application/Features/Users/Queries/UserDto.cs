namespace RealEstatePropertyListingPlatform.Application.Features.Users.Queries
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
    }
}
