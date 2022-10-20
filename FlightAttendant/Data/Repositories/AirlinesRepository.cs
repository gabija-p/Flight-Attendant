using FlightAttendant.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightAttendant.Data.Repositories
{
    public interface IAirlinesRepository
    {
        Task CreateAsync(Airline airline);
        Task<IReadOnlyList<Airline>> GetManyAsync(int airportId);
        Task<Airline?> GetOneAsync(int airlineId, int airportId);
        Task RemoveAsync(Airline airline);
        Task UpdateAsync(Airline airline);
    }

    public class AirlinesRepository : IAirlinesRepository
    {
        private readonly FlightsDbContext _flightsDbcontext;
        public AirlinesRepository(FlightsDbContext flightsDbContext)
        {
            _flightsDbcontext = flightsDbContext;
        }

        public async Task<Airline?> GetOneAsync(int airlineId, int airportId)
        {
            return await _flightsDbcontext.Airlines.FirstOrDefaultAsync(o => o.Id == airlineId && o.AirportId == airportId);
        }

        public async Task<IReadOnlyList<Airline>> GetManyAsync(int airportId)
        {
            return await _flightsDbcontext.Airlines.Where(o => o.AirportId == airportId).ToListAsync();
        }

        public async Task CreateAsync(Airline airline)
        {
            _flightsDbcontext.Airlines.AddAsync(airline);
            await _flightsDbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Airline airline)
        {
            _flightsDbcontext.Airlines.Update(airline);
            await _flightsDbcontext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Airline airline)
        {
            _flightsDbcontext.Airlines.Remove(airline);
            await _flightsDbcontext.SaveChangesAsync();
        }
    }
}
