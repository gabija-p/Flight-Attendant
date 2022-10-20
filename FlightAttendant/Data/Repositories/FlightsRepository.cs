using FlightAttendant.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightAttendant.Data.Repositories
{
    public interface IFlightsRepository
    {
        Task CreateAsync(Flight flight);
        Task<IReadOnlyList<Flight>> GetManyAsync(int airportId, int airlineId);
        Task<Flight?> GetOneAsync(int airportId, int airlineId, int flightId);
        Task RemoveAsync(Flight flight);
        Task UpdateAsync(Flight flight);
    }

    public class FlightsRepository : IFlightsRepository
    {
        private readonly FlightsDbContext _flightsDbContext;

        public FlightsRepository(FlightsDbContext flightsDbContext)
        {
            _flightsDbContext = flightsDbContext;
        }

        public async Task<Flight?> GetOneAsync(int airportId, int airlineId, int flightId)
        {
            return await _flightsDbContext.Flights.FirstOrDefaultAsync(o => o.Id == flightId && o.AirlineId == airlineId);
        }

        public async Task<IReadOnlyList<Flight>> GetManyAsync(int airportId, int airlineId)
        {
            return await _flightsDbContext.Flights.Where(o => o.AirlineId == airlineId && o.AirportId == airportId).ToListAsync();
        }

        public async Task CreateAsync(Flight flight)
        {
            _flightsDbContext.Flights.AddAsync(flight);
            await _flightsDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            _flightsDbContext.Flights.Update(flight);
            await _flightsDbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Flight flight)
        {
            _flightsDbContext.Flights.Remove(flight);
            await _flightsDbContext.SaveChangesAsync();
        }

    }
}
