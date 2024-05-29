using Microsoft.EntityFrameworkCore;
using MyWebServer.App.Data.Models;

namespace MyWebServer.App.Data
{
    public class SharedTripContext : DbContext
    {
        public SharedTripContext()
        {
                
        }

        public SharedTripContext(DbContextOptions option)
            :base(option)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SharedTrips;Integrated Security = true;TrustServerCertificate=True;");
            }
        }
    }
}
