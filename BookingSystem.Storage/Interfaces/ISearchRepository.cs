using BookingSystem.Models.ViewModels;

namespace BookingSystem.Storage.Interfaces
{
    public interface ISearchRepository : IInMemoryRepository<SearchRes>
    {
    }
}