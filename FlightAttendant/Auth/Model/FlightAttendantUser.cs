using Microsoft.AspNetCore.Identity;

namespace FlightAttendant.Auth.Model
{
    public class FlightAttendantUser : IdentityUser
    {
        [PersonalData]
        public string? AdditionalInfo { get; set; }
    }
}
