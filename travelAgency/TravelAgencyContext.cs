using Microsoft.EntityFrameworkCore;
using travelAgency.model;

namespace travelAgency
{
    public class TravelAgencyContext : DbContext
    {
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<TripSchedule> TripSchedules { get; set; }
        public DbSet<Arrangement> Arrangements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString;
            //if (optionsBuilder.IsConfigured)
            //{
            //    // Migrations
            //    connectionString = "Data Source=database\\database.sqlite";
            //}
            //else
            //{
            //    // Running the app
            //    connectionString = "Data Source=..\\..\\..\\database\\database.sqlite";
            //}

            //optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.UseSqlite("Data Source=..\\..\\..\\database\\database.sqlite"); //za pokretanje
            //optionsBuilder.UseSqlite("Data Source=database\\database.sqlite"); //za migracije
        }
    }
}