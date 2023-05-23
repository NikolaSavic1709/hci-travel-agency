using Microsoft.EntityFrameworkCore;
using travelAgency.model;

namespace travelAgency
{
    public class TravelAgencyContext : DbContext
    {
        public DbSet<Journey> Journeys { get; set; }
        public DbSet<Place> Places { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=..\\..\\..\\database\\database.sqlite"); //za pokretanje
            //optionsBuilder.UseSqlite("Data Source=database\\database.sqlite"); //za migracije
        }
    }
}
