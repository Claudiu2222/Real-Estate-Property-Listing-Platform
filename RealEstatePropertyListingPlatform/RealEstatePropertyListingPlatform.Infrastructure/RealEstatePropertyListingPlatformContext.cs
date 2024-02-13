using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Npgsql;
using RealEstatePropertyListingPlatform.Application.Contracts.Interfaces;
using RealEstatePropertyListingPlatform.Domain.Common;
using RealEstatePropertyListingPlatform.Domain.Entities;
using RealEstatePropertyListingPlatform.Domain.Enums;


namespace RealEstatePropertyListingPlatform.Infrastructure
{
    public class RealEstatePropertyListingPlatformContext : DbContext
    {
        // public RealEstatePropertyListingPlatformContext(
        //     DbContextOptions<RealEstatePropertyListingPlatformContext> options, DbSet<User> users, DbSet<Property> properties, DbSet<Listing> listings):
        //     base(options)
        // {
        //     Users = users;
        //     Properties = properties;
        //     Listings = listings;
        // }

        private readonly ICurrentUserService currentUserService;
        private readonly string apiKey;

        public RealEstatePropertyListingPlatformContext()
        {}
        public RealEstatePropertyListingPlatformContext(DbContextOptions<RealEstatePropertyListingPlatformContext> options, ICurrentUserService currentUserService, string apiKey) : base(options)
        {
            this.currentUserService = currentUserService;
            this.apiKey = apiKey;
        }

        //public DbSet<User> Users { get; set; } excluded from project - Old-Users


        public DbSet<Property> Properties { get; set; }
        public DbSet<Listing> Listings { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Listing>()
                .OwnsOne(listing => listing.Price, price =>
                {
                    price.Property(p => p.Value).HasColumnName("PriceValue");
                    price.Property(p => p.Currency).HasColumnName("PriceCurrency");
                });
/*            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.PhoneNumber).IsUnique(); excludeed from project - OldUsers*/
        }

        /*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                    optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=RealEstatePropertyListingPlatform;UserId=postgres;Password=1234;");
                }*/

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<IReadOnlyList<Listing>> FilterListings(decimal priceLowerBound, decimal priceUpperBound, int currency,
                                        string city, string region, int propertyType, 
                                        int squareFeetLowerBound, int squareFeetUpperBound,
                                        bool? forRent,
                                        string containsInTitle)
        {
            /*
                1. if both priceLowerBound and priceUpperBound are 0, then we don't need to filter by price
                2. is city is null, then we don't filter by city
                3. if region is null, then we don't filter by region
                4. if propertyType is -1, then we don't filter by propertyType
                5. if squareFeetLowerBound and sqareFeetUpperBound are 0, then we don't filter by squareFeet
                6. if forRent is null, then we don't filter by forRent
                7. if containsInTitle is null, then we don't filter by title
             */
            decimal currencyToUsd = 1;
            decimal currencyToEur = 1;
            decimal currencyToRon = 1;

            if (!(priceLowerBound == 0 && priceUpperBound == 0))
            {
                string currencyString = Enum.GetName(typeof(Currency), currency)!;
                Tuple<decimal, decimal, decimal> curr = await GetCurrency(currencyString);

                currencyToUsd = curr.Item1;
                currencyToEur = curr.Item2;
                currencyToRon = curr.Item3;
            }

            // Define the SQL query with parameters
            string sqlQuery = "SELECT * FROM filter_listings(@currency_to_USD, @currency_to_EUR, @currency_to_RON, " +
                              "@price_lower_bound, @price_upper_bound, @city, @region, @property_type, " +
                              "@square_feet_lower_bound, @square_feet_upper_bound, @for_rent, @contains_in_title)";

            // Execute the SQL query with parameters
            var result = await Listings
                .FromSqlRaw(sqlQuery,
                    new NpgsqlParameter("currency_to_USD", currencyToUsd),
                    new NpgsqlParameter("currency_to_EUR", currencyToEur),
                    new NpgsqlParameter("currency_to_RON", currencyToRon),
                    new NpgsqlParameter("price_lower_bound", priceLowerBound),
                    new NpgsqlParameter("price_upper_bound", priceUpperBound),
                    new NpgsqlParameter("city", city ?? (object)DBNull.Value), // Convert null to DBNull.Value
                    new NpgsqlParameter("region", region ?? (object)DBNull.Value),
                    new NpgsqlParameter("property_type", propertyType),
                    new NpgsqlParameter("square_feet_lower_bound", squareFeetLowerBound),
                    new NpgsqlParameter("square_feet_upper_bound", squareFeetUpperBound),
                    new NpgsqlParameter("for_rent", forRent ?? (object)DBNull.Value),
                    new NpgsqlParameter("contains_in_title", containsInTitle ?? (object)DBNull.Value))
                .ToListAsync();

            return result;

        }

        private async Task<Tuple<decimal, decimal, decimal>> GetCurrency(string currency)
        {

            // Create an instance of HttpClient
            using (var httpClient = new HttpClient())
            {
                // Set the base address of the API
                httpClient.BaseAddress = new Uri($"https://v6.exchangerate-api.com/v6/{this.apiKey}/latest/{currency}");

                try
                {
                    // Send a GET request to the API endpoint
                    HttpResponseMessage response = await httpClient.GetAsync("");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Parse the response JSON
                        JObject responseObject = JObject.Parse(responseBody);

                        // Check if the response contains the "result" field with a value of "success"
                        if (responseObject["result"]?.ToString() == "success")
                        {
                            // Extract the conversion rates
                            JObject conversionRates = (JObject)responseObject["conversion_rates"];

                            decimal usdRate = (decimal)conversionRates["USD"];
                            decimal eurRate = (decimal)conversionRates["EUR"];
                            decimal ronRate = (decimal)conversionRates["RON"];


                            // Output the conversion rates
                            Console.WriteLine($"USD to USD: {usdRate}");
                            Console.WriteLine($"USD to EUR: {eurRate}");
                            Console.WriteLine($"USD to RON: {ronRate}");

                            return new Tuple<decimal, decimal, decimal>(usdRate, eurRate, ronRate);

                        }
                        else
                        {
                            Console.WriteLine("API request failed.");
                            return new Tuple<decimal, decimal, decimal>(0, 0, 0);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to call the API. Status code: {response.StatusCode}");
                        return new Tuple<decimal, decimal, decimal>(0, 0, 0);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return new Tuple<decimal, decimal, decimal>(0, 0, 0);
                }
            }
        }



    }
}
