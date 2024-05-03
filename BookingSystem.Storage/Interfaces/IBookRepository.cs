using BookingSystem.Models.ViewModels;

namespace BookingSystem.Storage.Interfaces
{
    public interface IBookRepository : IInMemoryRepository<BookRes>
    {
        BookRes GetByBookingCode(string bookingCode);
    }
}