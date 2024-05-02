using BookingSystem.Models.ViewModels;

namespace BookingSystem.ApplicationService.Interfaces
{
    public interface IBookService
    {
        BookRes Book(BookReq bookReq);
    }
}