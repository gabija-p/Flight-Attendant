namespace FlightAttendant.Data.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
        public string Gate { get; set; }
        public double Price { get; set; }
        public int Seats { get; set; } //how many passengers allowed onboard
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        public int AirportId { get; set; }
    }
}
