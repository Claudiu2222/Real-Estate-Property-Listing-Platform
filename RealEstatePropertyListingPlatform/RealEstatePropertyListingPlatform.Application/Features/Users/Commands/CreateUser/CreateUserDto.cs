namespace RealEstatePropertyListingPlatform.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserDto
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; } = default!;
        public string? Password { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? FirstName { get; set; } = default!;
    }
}
