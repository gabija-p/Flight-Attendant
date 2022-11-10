using FlightAttendant.Auth.Model;
using FlightAttendant.Data.Dtos.Flights;
using FlightAttendant.Data.Entities;
using FlightAttendant.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightAttendant.Controllers 
{
    [ApiController]
    [Route("api/airports/{airportId}/airlines/{airlineId}/flights")]
    public class FlightsController : ControllerBase
    {
        private readonly IAirportsRepository _airportsRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IFlightsRepository _flightsRepository;
        private readonly IAuthorizationService _authorizationService;

        public FlightsController(IAirportsRepository airportsRepository, IAirlinesRepository airlinesRepository, IFlightsRepository flightsRepository,
            IAuthorizationService authorizationService)
        {
            _airportsRepository = airportsRepository;
            _airlinesRepository = airlinesRepository;
            _flightsRepository = flightsRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<FlightDto>> GetMany(int airportId, int airlineId)
        {
            var flights = await _flightsRepository.GetManyAsync(airportId, airlineId);
            return flights.Select(o => new FlightDto(o.Id, o.Destination, o.Year, o.Month, o.Day, o.Hour, o.Minutes, o.Gate, o.Price, o.Seats));
        }

        [HttpGet]
        [Route("{flightId}")]
        public async Task<ActionResult<FlightDto>> GetOne(int flightId, int airlineId, int airportId)
        {
            var flight = await _flightsRepository.GetOneAsync(airportId, airlineId, flightId);
            if (flight == null)
            {
                return NotFound($"Couldn't find a flight with id of {flightId} or airline with id of {airlineId} or airport with id of {airportId}");
            }

            return new FlightDto(flight.Id, flight.Destination, flight.Year, flight.Month, flight.Day, flight.Hour, flight.Minutes, flight.Gate, flight.Price, flight.Seats);
        }

        [HttpPost]
        public async Task<ActionResult<FlightDto>> Create(int airlineId, int airportId, CreateFlightDto createFlightDto)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);
            if (airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, airport, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return NotFound("");
            }

            var airline = await _airlinesRepository.GetOneAsync(airlineId, airportId);
            if (airline == null)
            {
                return NotFound($"Couldn't find an airline with id of {airlineId}");
            }

            var flight = new Flight();
            flight.Destination = createFlightDto.destination;
            flight.Year = createFlightDto.year;
            flight.Month = createFlightDto.month;
            flight.Day = createFlightDto.day;
            flight.Hour = createFlightDto.hour;
            flight.Minutes = createFlightDto.minutes;
            flight.Gate = createFlightDto.gate;
            flight.Price = createFlightDto.price;
            flight.Seats = createFlightDto.seats;
            flight.AirlineId = airlineId;
            flight.AirportId = airportId;

            await _flightsRepository.CreateAsync(flight);
            return Created($"/api/airports/{airportId}/airlines/{airline.Id}/flights/{flight.Id}", 
                new FlightDto(flight.Id, flight.Destination, flight.Year, flight.Month, flight.Day, flight.Hour, flight.Minutes, flight.Gate, flight.Price, flight.Seats));
        }

        [HttpPut]
        [Route("{flightId}")]
        public async Task<ActionResult<FlightDto>> Update(int airportId, int airlineId, int flightId, UpdateFlightDto updateFlightDto)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);
            if (airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, airport, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return NotFound("");
            }

            var airline = await _airlinesRepository.GetOneAsync(airlineId, airportId);
            if (airline == null)
            {
                return NotFound($"Couldn't find an airline with id of {airlineId}");
            }

            var flight = await _flightsRepository.GetOneAsync(airportId, airlineId, flightId);
            if (flight == null)
            {
                return NotFound($"Couldn't find a flight with id of {flightId}");
            }

            flight.Month = updateFlightDto.month;
            flight.Day = updateFlightDto.day;
            flight.Hour = updateFlightDto.hour;
            flight.Minutes = updateFlightDto.minutes;
            flight.Gate = updateFlightDto.gate;
            flight.Price = updateFlightDto.price;

            await _flightsRepository.UpdateAsync(flight);
            return Ok(new FlightDto(flight.Id, flight.Destination, flight.Year, flight.Month, flight.Day, flight.Hour, flight.Minutes, flight.Gate, flight.Price, flight.Seats));
        }

        [HttpDelete]
        [Route("{flightId}")]
        public async Task<ActionResult> Remove(int airportId, int airlineId, int flightId)
        {
            var airport = await _airportsRepository.GetOneAsync(airportId);
            if (airport == null)
            {
                return NotFound($"Couldn't find an airport with id of {airportId}");
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, airport, PolicyNames.ResourceOwner);

            if (!authorizationResult.Succeeded)
            {
                return NotFound("");
            }

            var airline = await _airlinesRepository.GetOneAsync(airlineId, airportId);
            if (airline == null)
            {
                return NotFound($"Couldn't find an airline with id of {airlineId}");
            }

            var flight = await _flightsRepository.GetOneAsync(airportId, airlineId, flightId);
            if (flight == null)
            {
                return NotFound($"Couldn't find a flight with id of {flightId}");
            }

            await _flightsRepository.RemoveAsync(flight);

            return NoContent();
        }
    }
}
