using BookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckStatusController : ControllerBase
    {
        [HttpGet("{bookingCode}")]
        public IActionResult CheckStatus(string bookingCode)
        {
            return Ok(bookingCode);
        }
    }
}
