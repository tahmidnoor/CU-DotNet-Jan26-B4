using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.TrackingService.Controllers
{
    [ApiController]
    [Route("api/tracking")]
    public class TrackingController : ControllerBase
    {
        // 🔒 Only Manager can access
        [HttpGet("gps")]
        [Authorize(Roles = "Manager")]
        public IActionResult GetGpsData()
        {
            return Ok(new
            {
                message = "Secure GPS Data",
                data = new[]
                {
                    new { TruckId = 1, Location = "Delhi", Status = "On Route" },
                    new { TruckId = 2, Location = "Mumbai", Status = "Stopped" }
                }
            });
        }
    }
}