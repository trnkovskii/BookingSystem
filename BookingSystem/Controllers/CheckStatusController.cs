using BookingSystem.ApplicationService.FluentValidation;
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
        private readonly CheckStatusReqValidator _checkStatusReqValidator;

        public CheckStatusController(ICheckStatusService checkStatusService)
        {
            _checkStatusService = checkStatusService;
            _checkStatusReqValidator = new CheckStatusReqValidator();
        }

        [HttpGet("{bookingCode}")]
        public async Task<IActionResult> CheckStatus(string bookingCode)
        {
            try
            {
                CheckStatusReq statusReq = new()
                {
                    BookingCode = bookingCode
                };

                var validationResult = await _checkStatusReqValidator.ValidateAsync(statusReq);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(errorMessages);
                }

                // this method call can be async if we use a real database in the future
                var result = _checkStatusService.CheckStatus(statusReq);

                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
