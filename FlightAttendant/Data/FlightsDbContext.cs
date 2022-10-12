using FlightAttendant.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightAttendant.Data
{
    public class FlightsDbContext : DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FlightsDb");
        }
    }
}
