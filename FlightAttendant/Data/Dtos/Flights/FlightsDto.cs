using FlightAttendant.Data.Entities;

namespace FlightAttendant.Data.Dtos.Flights
{
    public record FlightDto(int id, string destination, int year, string month, int day, int hour, int minutes, string gate, double price, int seats);
    public record CreateFlightDto(string destination, int year, string month, int day, int hour, int minutes, string gate, double price, int seats);
    public record UpdateFlightDto(string month, int day, int hour, int minutes, string gate, double price);
}
