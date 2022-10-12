using FlightAttendant.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightAttendant.Data.Repositories
{
    public interface IAirportsRepository
    {
        Task CreateAsync(Airport airport);
        Task<IReadOnlyList<Airport?>> GetManyAsync();
        Task<Airport?> GetOneAsync(int airportId);
        Task RemoveAsync(Airport airport);
        Task UpdateAsync(Airport airport);
    }

    public class AirportsRepository : IAirportsRepository
    {
        private readonly FlightsDbContext _flightsDbContext;

        public AirportsRepository(FlightsDbContext flightsDbContext)
        {
            _flightsDbContext = flightsDbContext;
        }

        public async Task<Airport?> GetOneAsync(int airportId)
        {
            return await _flightsDbContext.Airports.FirstOrDefaultAsync(o => o.Id == airportId);
        }

        public async Task<IReadOnlyList<Airport?>> GetManyAsync()
        {
            return await _flightsDbContext.Airports.ToListAsync();
        }

        public async Task CreateAsync(Airport airport)
        {
            _flightsDbContext.Airports.AddAsync(airport);
            await _flightsDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Airport airport)
        {
            _flightsDbContext.Airports.Update(airport);
            await _flightsDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Airport airport)
        {
            _flightsDbContext.Airports.Remove(airport);
            await _flightsDbContext.SaveChangesAsync();
        }
    }
}
