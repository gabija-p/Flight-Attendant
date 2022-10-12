namespace FlightAttendant.Data.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public string Gate { get; set; }
        public double Price { get; set; }
        public int Seats { get; set; } //how many passengers allowed onboard

        public Airline Airline { get; set; }
    }
}
