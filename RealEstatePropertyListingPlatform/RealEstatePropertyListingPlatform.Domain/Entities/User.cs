using System.IO.Compression;
using RealEstatePropertyListingPlatform.Domain.ClassValidators;
using RealEstatePropertyListingPlatform.Domain.Common;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class User : AuditableEntity
    {
        public Guid UserId { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? LastName { get; private set; }
        public string? FirstName { get; private set; }

        public List<Listing>? Listings { get; private set; }

        public List<Property>? Properties { get; private set; }
        
        private User() {}

        public static Result<User> Create(string email, string password, string lastName, string firstName)
        {

            var error = UserValidator.ValidateUser(email, password, lastName, firstName);

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
                FirstName = firstName
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
        
        public Result<User> UpdateEmail(string email)
        {
            var error = UserValidator.ValidateString(email, nameof(email));
            if (!string.IsNullOrWhiteSpace(error)) return Result<User>.Failure(error);
            Email = email;
            return Result<User>.Success(this);
        }
        
        public Result<User> UpdatePassword(string password) // encrypt password
        {
            var error = UserValidator.ValidateString(password, nameof(password));
            if (!string.IsNullOrWhiteSpace(error)) return Result<User>.Failure(error);
            Password = password;
            return Result<User>.Success(this);
        }
        
        public Result<User> UpdateLastName(string lastName)
        {
            var error = UserValidator.ValidateString(lastName, nameof(lastName));
            if (!string.IsNullOrWhiteSpace(error)) return Result<User>.Failure(error);
            LastName = lastName;
            return Result<User>.Success(this);
        }

        public Result<User> UpdateFirstName(string firstName)
        {
            var error = UserValidator.ValidateString(firstName, nameof(firstName));
            if (!string.IsNullOrWhiteSpace(error)) return Result<User>.Failure(error);
            FirstName = firstName;
            return Result<User>.Success(this);

        }
    }
}
