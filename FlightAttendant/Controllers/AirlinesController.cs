using FlightAttendant.Data.Dtos.Airlines;
using FlightAttendant.Data.Entities;
using FlightAttendant.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace FlightAttendant.Controllers
{
    [ApiController]
    [Route("api/airports/{airportId}/airlines")]
    public class AirlinesController : ControllerBase
    {
        private readonly IAirportsRepository _airportsRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        public AirlinesController(IAirportsRepository airportsRepository, IAirlinesRepository airlinesRepository)
        {
            _airportsRepository = airportsRepository;
            _airlinesRepository = airlinesRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AirlineDto>> GetMany(int airportId)
        {
            var airlines = await _airlinesRepository.GetManyAsync(airportId);
            return airlines.Select(o => new AirlineDto(o.Id, o.Name));
        }

        [HttpGet]
        [Route("{airlineId}")]
        public async Task<ActionResult<AirlineDto>> GetOne(int airportId, int airlineId)
        {
            var airline = await _airlinesRepository.GetOneAsync(airportId, airlineId);
            if(airline == null)
            {
                return NotFound($"Couldn't find an airline with id of {airlineId} or airport with id of {airportId}");
            }

            return new AirlineDto(airline.Id, airline.Name);
        }

        [HttpPost]
        public async Task<ActionResult<AirlineDto>> Create(int airportId, CreateAirlineDto createAirlineDto)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);
            if(airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var airline = new Airline();
            airline.Name = createAirlineDto.Name;
            airline.AirportId = airportId;

            await _airlinesRepository.CreateAsync(airline);
            return Created($"/api/airports/{airportId}/airlines/{airline.Id}", new AirlineDto(airline.Id, airline.Name));
        }

        [HttpPut]
        [Route("{airlineId}")]
        public async Task<ActionResult<AirlineDto>> Update(int airportId, int airlineId, UpdateAirlineDto updateAirlineDto)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);
            if(airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var airline = await _airlinesRepository.GetOneAsync(airlineId, airportId);
            if (airline == null)
            {
                return NotFound($"Couldn't find an airline with id of {airlineId}");
            }

            airline.Name = updateAirlineDto.Name;

            return Ok(new AirlineDto(airline.Id, airline.Name));
        }

        [HttpDelete]
        [Route("{airlineId}")]
        public async Task<ActionResult> Remove(int airportId, int airlineId)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);
            if (airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var airline = await _airlinesRepository.GetOneAsync(airlineId, airportId);
            if (airline == null)
            {
                return NotFound($"Couldn't find an airline with id of {airlineId}");
            }

            await _airlinesRepository.RemoveAsync(airline);

            return NoContent();
        }
    }
}
