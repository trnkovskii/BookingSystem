using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckStatusController : ControllerBase
    {
        private readonly ICheckStatusService _checkStatusService;

        public CheckStatusController(ICheckStatusService checkStatusService)
        {
            _checkStatusService = checkStatusService;
        }

        [HttpGet("{bookingCode}")]
        public IActionResult CheckStatus(string bookingCode)
        {
            CheckStatusReq statusReq = new()
            {
                BookingCode = bookingCode
            };

            var result = _checkStatusService.CheckStatus(statusReq);

            return Ok(result);
        }
    }
}
