using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlightAttendant.Pages
{
    public class AirportsModel : PageModel
    {
        private readonly ILogger<AirportsModel> _logger;

        public AirportsModel(ILogger<AirportsModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
