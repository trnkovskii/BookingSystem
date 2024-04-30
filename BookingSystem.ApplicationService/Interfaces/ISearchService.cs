using BookingSystem.Models.ViewModels;

namespace BookingSystem.ApplicationService.Interfaces
{
    public interface ISearchService
    {
        Task<SearchRes> Search(SearchReq searchReq);
    }
}