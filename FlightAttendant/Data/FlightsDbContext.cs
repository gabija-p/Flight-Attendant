using FlightAttendant.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FlightAttendant.Auth.Model;
using Microsoft.Extensions.Configuration;

namespace FlightAttendant.Data
{
    public class FlightsDbContext : IdentityDbContext<FlightAttendantUser>
    {
        private readonly IConfiguration _configuration;
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }

        public FlightsDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(_configuration.GetValue<string>("PostgreSQLConnectionString"));
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FlightsDb");
        }
    }
}
