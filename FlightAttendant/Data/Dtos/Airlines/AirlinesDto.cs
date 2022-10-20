using FlightAttendant.Data.Entities;

namespace FlightAttendant.Data.Dtos.Airlines
{
    public record AirlineDto(int id, string Name);
    public record CreateAirlineDto(string Name);
    public record UpdateAirlineDto(string Name);
}
