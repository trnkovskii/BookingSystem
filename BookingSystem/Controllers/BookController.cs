using BookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        [HttpPost]
        public IActionResult Book(BookReq bookReq)
        {
            return Ok(bookReq);
        }
    }
}
