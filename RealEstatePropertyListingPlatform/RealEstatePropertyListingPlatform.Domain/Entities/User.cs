using RealEstatePropertyListingPlatform.Domain.ClassValidators;
using RealEstatePropertyListingPlatform.Domain.Common;

namespace RealEstatePropertyListingPlatform.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }

        public List<Listing>? Listings { get; private set; }

        public List<Property>? Properties { get; private set; }



        private User()
        {

        }

        public static Result<User> Create(string email, string password, string LastName, string FirstName)
        {

            string error = UserValidator.ValidateUser(email, password, LastName, FirstName);

            if (string.IsNullOrWhiteSpace(error))
            {
                return Result<User>.Failure(error);
            }

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Password = password,
                LastName = LastName,
                FirstName = FirstName
            };

            return Result<User>.Success(user);
        }

        public void AttachListing(Listing listing)
        {
            if (Listings == null)
            {
                Listings = new List<Listing>();
            }

            Listings.Add(listing);
        }

        public void AttachProperty(Property property)
        {
            if (Properties == null)
            {
                Properties = new List<Property>();
            }

            Properties.Add(property);
        }
    }
}
