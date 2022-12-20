using FlightAttendant.Auth.Model;
using FlightAttendant.Data.Dtos.Airports;
using FlightAttendant.Data.Entities;
using FlightAttendant.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace FlightAttendant.Controllers
{
    [ApiController]
    [Route("api/airports")]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportsRepository _airportsRepository;
        private readonly IAuthorizationService _authorizationService;
        public AirportsController(IAirportsRepository airportsRepository, IAuthorizationService authorizationService)
        {
            _airportsRepository = airportsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<AirportDto>> GetMany()
        {
            var airports = await _airportsRepository.GetManyAsync();
            return airports.Select(o => new AirportDto(o.Id, o.Name, o.Location, o.UserId));
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
        [Authorize(Roles = FlightAttendantRoles.AttendantUser)]
        public async Task<ActionResult<AirportDto>> Create(CreateAirportDto createAirportDto)
        {
            var airport = new Airport {Name = createAirportDto.Name, Location = createAirportDto.Location, UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)};
            await _airportsRepository.CreateAsync(airport);


            //201
            return Created($"/api/airports/{airport.Id}/", new AirportDto(airport.Id, airport.Name, airport.Location));
            //return CreatedAtAction("GetAirport", new { airportId = airport.Id }, new AirportDto(airport.Id, airport.Name, airport.Location));
        }

        [HttpPut]
        [Route("{airportId}")]
        [Authorize(Roles = FlightAttendantRoles.AttendantUser)]
        public async Task<ActionResult<AirportDto>> Update(int airportId, UpdateAirportDto updateAirportDto)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);

            //404
            if (airport == null)
            {
                return NotFound($"Couldn't find am airport with id of {airportId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, airport, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return NotFound("");
            }

            airport.Name = updateAirportDto.Name;
            airport.Location = updateAirportDto.Location;
            await _airportsRepository.UpdateAsync(airport);

            return Ok(new AirportDto(airport.Id, airport.Name, airport.Location));
        }

        [HttpDelete]
        [Route("{airportId}")]
        [Authorize(Roles = FlightAttendantRoles.AttendantUser)]
        public async Task<ActionResult> Remove(int airportId)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);

            //404
            if (airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, airport, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return NotFound("");
            }

            await _airportsRepository.RemoveAsync(airport);

            //204
            return NoContent();

        }
    }
}
