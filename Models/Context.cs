using Microsoft.EntityFrameworkCore;

namespace TravelNow.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Address> Addresses {get; set;}
        public DbSet<Listing> Listings {get; set;}
        public DbSet<Booking> Bookings {get; set;}
        public DbSet<ListingReview> ListingReviews {get; set;}
    }
}