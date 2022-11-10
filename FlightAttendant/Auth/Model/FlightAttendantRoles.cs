namespace FlightAttendant.Auth.Model
{
    public static class FlightAttendantRoles
    {
        public const string Admin = nameof(Admin);
        public const string AttendantUser = nameof(AttendantUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, AttendantUser };
    }
}
