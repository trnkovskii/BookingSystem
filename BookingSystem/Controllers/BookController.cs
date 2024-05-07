using BookingSystem.ApplicationService.FluentValidation;
using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly BookReqValidator _bookReqValidator;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
            _bookReqValidator = new BookReqValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookReq bookReq)
        {
            try
            {
                var validationResult = await _bookReqValidator.ValidateAsync(bookReq);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(errorMessages);
                }

                // this method call can be async if we use a real database in the future
                var bookRes = _bookService.Book(bookReq);

                return Ok(bookRes);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
