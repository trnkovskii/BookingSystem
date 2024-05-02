using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.Storage.Repositories
{
    public class SearchRepository : InMemoryRepository<SearchRes>, ISearchRepository
    {
    }
}
