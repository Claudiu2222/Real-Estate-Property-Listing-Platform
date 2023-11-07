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

        public static Result<User> Create(Guid id, string email, string password)
        {

        }
    }
}
