namespace FlightAttendant.Data.Dtos.Airports
{
    public record AirportDto(int id, string Name, string Location);
    public record CreateAirportDto(string Name, string Location);
    public record UpdateAirportDto(string Name, string Location);
}
