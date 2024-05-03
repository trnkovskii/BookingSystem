using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.Storage.Repositories
{
    public class BookRepository : InMemoryRepository<BookRes>, IBookRepository
    {
        public BookRes GetByBookingCode(string bookingCode)
        {
            var res = FindById("BookingCode", bookingCode);
            return res;
        }
    }
}
