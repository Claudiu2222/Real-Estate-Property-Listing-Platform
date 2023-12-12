using RealEstatePropertyListingPlatform.Domain.ClassValidators;
using RealEstatePropertyListingPlatform.Domain.Common;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Guid UserId { get; private set; }

        
        public string? Email { get; private set; } // should be unique
        public string? Password { get; private set; }
        public string? LastName { get; private set; }
        public string? FirstName { get; private set; }

        public string? PhoneNumber { get; private set; }

        public List<Listing>? Listings { get; private set; }

        public List<Property>? Properties { get; private set; }
        
        private User() {}

        public static Result<User> Create(string email, string password, string lastName, string firstName, string phoneNumber)
        {

            var error = UserValidator.ValidateUser(email, password, lastName, firstName, phoneNumber);

            if (!string.IsNullOrWhiteSpace(error))
            {
                return Result<User>.Failure(error);
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Password = password,
                LastName = lastName,
                FirstName = firstName,
                PhoneNumber = phoneNumber
            };

            return Result<User>.Success(user);
        }

        public void AttachListing(Listing? listing)
        {
            if (listing == null)
            {
                return;
            }
            
            Listings ??= new List<Listing>();

            Listings.Add(listing);
        }

        public void AttachProperty(Property? property)
        {
            if (property == null)
            {
                return;
            }
            
            Properties ??= new List<Property>();

            Properties.Add(property);
        }
        
        public void Update(string requestEmail, string requestPassword, string requestFirstName, string requestLastName, string requestPhoneNumber)
        {
            Email = requestEmail;
            Password = requestPassword;
            FirstName = requestFirstName;
            LastName = requestLastName;
            PhoneNumber = requestPhoneNumber;
        }
    }
}
