using FlightAttendant.Data.Dtos.Airports;
using FlightAttendant.Data.Entities;
using FlightAttendant.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightAttendant.Controllers
{
    [ApiController]
    [Route("api/airports")]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportsRepository _airportsRepository;
        public AirportsController(IAirportsRepository airportsRepository)
        {
            _airportsRepository = airportsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AirportDto>> GetMany()
        {
            var airports = await _airportsRepository.GetManyAsync();
            return airports.Select(o => new AirportDto(o.Id, o.Name, o.Location));
        }

        [HttpGet]
        [Route("{airportId}", Name = "GetAirport")]
        public async Task<ActionResult<AirportDto>> GetOne(int airportId)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);

            //404
            if(airport == null)
            {
                return NotFound($"Couldn't find am airport with id of {airportId}");
            }

            return new AirportDto(airport.Id, airport.Name, airport.Location);
        }

        [HttpPost]
        public async Task<ActionResult<AirportDto>> Create(CreateAirportDto createAirportDto)
        {
            var airport = new Airport {Name = createAirportDto.Name, Location = createAirportDto.Location};
            await _airportsRepository.CreateAsync(airport);

            //201
            return Created($"/api/airports/{airport.Id}/", new AirportDto(airport.Id, airport.Name, airport.Location));
            //return CreatedAtAction("GetAirport", new { airportId = airport.Id }, new AirportDto(airport.Id, airport.Name, airport.Location));
        }

        [HttpPut]
        [Route("{airportId}")]
        public async Task<ActionResult<AirportDto>> Update(int airportId, UpdateAirportDto updateAirportDto)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);

            //404
            if (airport == null)
            {
                return NotFound($"Couldn't find am airport with id of {airportId}");
            }

            airport.Name = updateAirportDto.Name;
            airport.Location = updateAirportDto.Location;
            await _airportsRepository.UpdateAsync(airport);

            return Ok(new AirportDto(airport.Id, airport.Name, airport.Location));
        }

        [HttpDelete]
        [Route("{airportId}")]
        public async Task<ActionResult> Remove(int airportId)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);

            //404
            if (airport == null)
            {
                return NotFound($"Couldn't find am airport with id of {airportId}");
            }

            await _airportsRepository.RemoveAsync(airport);

            //204
            return NoContent();

        }
    }
}
