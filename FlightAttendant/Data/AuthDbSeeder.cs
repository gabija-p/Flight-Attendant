using FlightAttendant.Auth.Model;
using Microsoft.AspNetCore.Identity;

namespace FlightAttendant.Data
{
    public class AuthDbSeeder
    {
        private readonly UserManager<FlightAttendantUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthDbSeeder(UserManager<FlightAttendantUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await AddDefaultRoles();
            await AddAdminUser();
        }

        private async Task AddAdminUser()
        {
            var newAdminUser = new FlightAttendantUser
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var exsistingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if(exsistingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, FlightAttendantRoles.All);
                }
            }
        }

        private async Task AddDefaultRoles()
        {
            foreach(var role in FlightAttendantRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
