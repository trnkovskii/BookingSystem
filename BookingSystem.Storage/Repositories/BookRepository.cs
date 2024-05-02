using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.Storage.Repositories
{
    public class BookRepository : InMemoryRepository<BookRes>, IBookRepository
    {
    }
}
