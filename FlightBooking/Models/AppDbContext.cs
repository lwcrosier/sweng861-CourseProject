using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Models
{
    /*
     * Class to represent database access for EntityFramework.
     * Dataset for Trip and SearchResults objects
     */
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trips { get; set; } = null!;
        public DbSet<TripSearchResults> SearchResults { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }



        /*
        Manually create table for sqlite:
        
        CREATE TABLE "__EFMigrationsHistory" (
        "MigrationId" text NOT NULL,
        "ProductVersion" text NOT NULL,
        CONSTRAINT "PK_HistoryRow" PRIMARY KEY ("MigrationId"));
        */
    }
}
