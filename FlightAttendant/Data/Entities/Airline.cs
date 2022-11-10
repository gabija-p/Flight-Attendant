using FlightAttendant.Auth.Model;

namespace FlightAttendant.Data.Entities
{
    public class Airline
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int AirportId { get; set; }
        public Airport Airport { get; set; }
    }
}
