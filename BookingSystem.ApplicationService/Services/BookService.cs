using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.ApplicationService.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IRandomGeneratorService _randomGeneratorService;

        public BookService(IBookRepository bookRepository, ISearchRepository searchRepository, IRandomGeneratorService randomGeneratorService)
        {
            _bookRepository = bookRepository;
            _searchRepository = searchRepository;
            _randomGeneratorService = randomGeneratorService;
        }

        public BookRes Book(BookReq bookReq)
        {
            string bookingCode = _randomGeneratorService.GenerateUniqueCode();

            int sleepTime = _randomGeneratorService.GenerateRandomTimeBetween30And60();

            DateTime bookingTime = DateTime.Now;

            var bookRes = new BookRes
            {
                BookingCode = bookingCode,
                BookingTime = bookingTime,
                SleepTime = sleepTime,
            };

            _bookRepository.StoreData(bookRes);

            Option option = FindSelectedOption(bookReq);

            BookHotelOrFlight(bookRes, option);

            return bookRes;
        }

        private void BookHotelOrFlight(BookRes bookRes, Option option)
        {
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
        }

        private Option FindSelectedOption(BookReq bookReq)
        {
            List<SearchRes> options = _searchRepository.GetAllOptions();

            return options.SelectMany(item => item.Options).FirstOrDefault(opt => opt.OptionCode == bookReq.OptionCode);
        }

        private void BookHotel(Option option, BookRes bookRes)
        {
            bookRes.IsHotelBooked = true;
            _bookRepository.UpdateData(bookRes, b => b.BookingCode == bookRes.BookingCode);
            Console.WriteLine($"Hotel with code: {option.HotelCode} is booked!");
        }

        private void BookFlight(Option option, BookRes bookRes)
        {
            bookRes.IsFlightBooked = true;
            _bookRepository.UpdateData(bookRes, b => b.BookingCode == bookRes.BookingCode);
            Console.WriteLine($"Flight with code: {option.FlightCode} is booked!");
        }
    }
}
