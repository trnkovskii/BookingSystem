using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;
using Moq;

namespace BookingSystem.Tests.ApplicationService
{
    [TestClass]
    public class BookServiceTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<ISearchRepository> _searchRepositoryMock;
        private Mock<IRandomGeneratorService> _randomGeneratorServiceMock;
        private IBookService _bookService;

        [TestInitialize]
        public void BookServiceTestsInitialize()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _searchRepositoryMock = new Mock<ISearchRepository>();
            _randomGeneratorServiceMock = new Mock<IRandomGeneratorService>();
            _bookService = new BookService(_bookRepositoryMock.Object, _searchRepositoryMock.Object, _randomGeneratorServiceMock.Object);
        }

        [TestMethod]
        public void BookHotelAndReturnThatHotelIsBooked()
        {
            string bookingCode = "a123zA";
            int sleepTime = 47;
            DateTime bookingTime = DateTime.Now;

            var bookReq = new BookReq()
            {
                OptionCode = "1",
                SearchReq = new SearchReq() { }
            };

            var bookRes = new BookRes
            {
                BookingCode = bookingCode,
                BookingTime = bookingTime,
                SleepTime = sleepTime,
                IsFlightBooked = false,
                IsHotelBooked = true,
                IsLastMinuteReservation = false
            };

            Option[] options = new Option[]
            {
                new()
                {
                    OptionCode = "1",
                    FlightCode = null,
                    FlightNumber = null,
                    ArrivalAirport = null,
                    DepartureAirport = null,
                    Price = 0,
                    HotelCode = "8626",
                    HotelId = 0,
                    HotelName = "Alexandar Square Boutique Hotel",
                    HotelDestinationCode = null,
                    HotelCity = null,
                    IsLastMinuteReservation = false
                }
            };

            var searchResults = new List<SearchRes>()
            {
               new()
               {
                   Options = options
               }
            };

            _randomGeneratorServiceMock.Setup(x => x.GenerateUniqueCode()).Returns(bookingCode);
            _randomGeneratorServiceMock.Setup(x => x.GenerateRandomTimeBetween30And60()).Returns(sleepTime);
            _bookRepositoryMock.Setup(x => x.StoreData(bookRes));
            _searchRepositoryMock.Setup(x => x.GetAllOptions()).Returns(searchResults);
            _bookRepositoryMock.Setup(x => x.UpdateData(bookRes, It.IsAny<Func<BookRes, bool>>()))
                    .Callback<BookRes, Func<BookRes, bool>>((book, predicate) => { });

            var result = _bookService.Book(bookReq);

            Assert.IsNotNull(result);
            Assert.AreEqual(bookRes.BookingCode, result.BookingCode);
            Assert.AreEqual(bookRes.IsHotelBooked, result.IsHotelBooked);

            _randomGeneratorServiceMock.Verify(x => x.GenerateUniqueCode(), Times.Once);
            _randomGeneratorServiceMock.Verify(x => x.GenerateRandomTimeBetween30And60(), Times.Once);
            _searchRepositoryMock.Verify(x => x.GetAllOptions(), Times.Once);
        }

        [TestMethod]
        public void BookFlightAndReturnThatFlightIsBooked()
        {
            string bookingCode = "a123zA";
            int sleepTime = 47;
            DateTime bookingTime = DateTime.Now;

            var bookReq = new BookReq()
            {
                OptionCode = "1",
                SearchReq = new SearchReq() { }
            };

            var bookRes = new BookRes
            {
                BookingCode = bookingCode,
                BookingTime = bookingTime,
                SleepTime = sleepTime,
                IsFlightBooked = true,
                IsHotelBooked = false,
                IsLastMinuteReservation = false
            };

            Option[] options = new Option[]
            {
                new()
                {
                    OptionCode = "1",
                    FlightCode = "461",
                    FlightNumber = "SK 461",
                    ArrivalAirport = "SKP",
                    DepartureAirport = "OSL",
                    Price = 0,
                    HotelCode = null,
                    HotelId = 0,
                    HotelName = null,
                    HotelDestinationCode = null,
                    HotelCity = null,
                    IsLastMinuteReservation = false,
                }
            };

            var searchResults = new List<SearchRes>()
            {
               new()
               {
                   Options = options
               }
            };

            _randomGeneratorServiceMock.Setup(x => x.GenerateUniqueCode()).Returns(bookingCode);
            _randomGeneratorServiceMock.Setup(x => x.GenerateRandomTimeBetween30And60()).Returns(sleepTime);
            _bookRepositoryMock.Setup(x => x.StoreData(bookRes));
            _searchRepositoryMock.Setup(x => x.GetAllOptions()).Returns(searchResults);
            _bookRepositoryMock.Setup(x => x.UpdateData(bookRes, It.IsAny<Func<BookRes, bool>>()))
                    .Callback<BookRes, Func<BookRes, bool>>((book, predicate) => { });

            var result = _bookService.Book(bookReq);

            Assert.IsNotNull(result);
            Assert.AreEqual(bookRes.BookingCode, result.BookingCode);
            Assert.AreEqual(bookRes.IsFlightBooked, result.IsFlightBooked);

            _randomGeneratorServiceMock.Verify(x => x.GenerateUniqueCode(), Times.Once);
            _randomGeneratorServiceMock.Verify(x => x.GenerateRandomTimeBetween30And60(), Times.Once);
            _searchRepositoryMock.Verify(x => x.GetAllOptions(), Times.Once);
        }
    }
}
