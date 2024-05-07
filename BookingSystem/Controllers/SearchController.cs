using BookingSystem.ApplicationService.FluentValidation;
using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly SearchReqValidator _searchReqValidator;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
            _searchReqValidator = new SearchReqValidator();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchReq searchReq)
        {
            try
            {
                var validationResult = await _searchReqValidator.ValidateAsync(searchReq);

                if (!validationResult.IsValid)
                {
                    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                    return BadRequest(errorMessages);
                }

                SearchRes result = await _searchService.Search(searchReq);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
