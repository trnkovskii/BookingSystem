using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.ApplicationService.Services
{
    public class BookService : IBookService
    {
        private readonly Random _random;
        private readonly IBookRepository _bookRepository;
        private readonly ISearchRepository _searchRepository;

        public BookService(IBookRepository bookRepository, ISearchRepository searchRepository)
        {
            _random = new Random();
            _bookRepository = bookRepository;
            _searchRepository = searchRepository;
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

            _bookRepository.StoreData(bookRes);

            List<SearchRes> options = _searchRepository.GetAllOptions();

            Option option = options.SelectMany(item => item.Options).FirstOrDefault(opt => opt.OptionCode == bookReq.OptionCode);

            if (option != null)
            {
                if (!string.IsNullOrEmpty(option.HotelCode))
                {
                    BookHotel(option, bookRes);
                }
                else
                {
                    BookFlight(option, bookRes);
                }
            }

            return bookRes;
        }

        private void BookHotel(Option option, BookRes bookRes)
        {
            bookRes.IsHotelBooked = true;
            Console.WriteLine($"Hotel with code: {option.HotelCode} is booked!");
            _bookRepository.UpdateData(bookRes, b => b.BookingCode == bookRes.BookingCode);
        }

        private void BookFlight(Option option, BookRes bookRes)
        {
            bookRes.IsFlightBooked = true;
            Console.WriteLine($"Flight with code: {option.FlightCode} is booked!");
            _bookRepository.UpdateData(bookRes, b => b.BookingCode == bookRes.BookingCode);
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
