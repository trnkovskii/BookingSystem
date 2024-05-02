using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage;

namespace BookingSystem.ApplicationService.Services
{
    public class BookService : IBookService
    {
        private readonly Random _random;
        private readonly IInMemoryStorage<BookRes> _inMemoryStorage;

        public BookService(IInMemoryStorage<BookRes> inMemoryStorage)
        {
            _inMemoryStorage = inMemoryStorage;
            _random = new Random();
        }

        public BookRes Book(BookReq bookReq)
        {
            string bookingCode = GenerateBookingCode();

            int sleepTime = _random.Next(30, 60);

            DateTime bookingTime = DateTime.Now;

            var bookRes = new BookRes
            {
                BookingCode = bookingCode,
                BookingTime = bookingTime,
                SleepTime = sleepTime,
            };

            _inMemoryStorage.StoreData(bookRes);

            return bookRes;
        }

        private string GenerateBookingCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] code = new char[6];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chars[_random.Next(chars.Length)];
            }

            return new string(code);
        }
    }
}
