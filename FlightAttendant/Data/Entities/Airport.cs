using FlightAttendant.Auth.Model;

namespace FlightAttendant.Data.Entities
{
    public class Airport : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public FlightAttendantUser User { get; set; }
    }
}
