using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;
using Moq;

namespace BookingSystem.Tests.ApplicationService
{
    [TestClass]
    public class SearchServiceTests
    {
        private Mock<ISearchRepository> _searchRepositoryMock;
        private Mock<IGetSearchDataFromApiService> _getSearchDataFromApiServiceMock;
        private SearchService _searchService;

        [TestInitialize]
        public void SearchServiceTestsInitialize()
        {
            _searchRepositoryMock = new Mock<ISearchRepository>();
            _getSearchDataFromApiServiceMock = new Mock<IGetSearchDataFromApiService>();
            _searchService = new SearchService(_searchRepositoryMock.Object, _getSearchDataFromApiServiceMock.Object);
        }

        [TestMethod]
        public async Task SearchMethodAndReturnHotelsAndFlights()
        {
            SearchReq searchReq = new()
            {
                DepartureAirport = "SKP",
                Destination = "OSL",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
            };

            var flightOptions = new Option[]
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
                            HotelCity = null
                        }
                  };

            var hotelOptions = new Option[]
                  {
                      new()
                        {
                            OptionCode = "2",
                            FlightCode = null,
                            FlightNumber = null,
                            ArrivalAirport = null,
                            DepartureAirport = null,
                            Price = 0,
                            HotelCode = "8626",
                            HotelId = 0,
                            HotelName = "Alexandar Square Boutique Hotel",
                            HotelDestinationCode = null,
                            HotelCity = null
                        }
                  };

            SearchRes searchRes = new()
            {
                Options = flightOptions.Concat(hotelOptions).ToArray()
            };

            _getSearchDataFromApiServiceMock.Setup(x => x.GetHotels(searchReq.Destination)).ReturnsAsync(hotelOptions);
            _getSearchDataFromApiServiceMock.Setup(x => x.GetFlights(searchReq.DepartureAirport, searchReq.Destination)).ReturnsAsync(flightOptions);

            _searchRepositoryMock.Setup(x => x.StoreData(searchRes));

            var result = await _searchService.Search(searchReq);

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(searchRes.Options, result.Options);

            _getSearchDataFromApiServiceMock.Verify(x => x.GetHotels(searchReq.Destination), Times.Once);
            _getSearchDataFromApiServiceMock.Verify(x => x.GetFlights(searchReq.DepartureAirport, searchReq.Destination), Times.Once);
        }

        [TestMethod]
        public async Task SearchMethodAndReturnsLastMinuteHotels()
        {
            SearchReq searchReq = new()
            {
                DepartureAirport = null,
                Destination = "OSL",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
            };

            var hotelOptions = new Option[]
            {
                new()
                {
                    FlightCode = null,
                    FlightNumber = null,
                    ArrivalAirport = null,
                    DepartureAirport = null,
                    Price = 0,
                    HotelCode = "8626",
                    HotelId = 0,
                    HotelName = "Alexandar Square Boutique Hotel",
                    HotelDestinationCode = null,
                    HotelCity = null
                }
            };

            SearchRes searchRes = new()
            {
                Options = hotelOptions
            };

            _getSearchDataFromApiServiceMock.Setup(x => x.GetHotels(searchReq.Destination)).ReturnsAsync(hotelOptions);
            _searchRepositoryMock.Setup(x => x.StoreData(searchRes));

            var result = await _searchService.Search(searchReq);

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(searchRes.Options, result.Options);

            _getSearchDataFromApiServiceMock.Verify(x => x.GetHotels(searchReq.Destination), Times.Once);
        }

        [TestMethod]
        public async Task SearchMethodAndReturnsHotels()
        {
            SearchReq searchReq = new()
            {
                DepartureAirport = null,
                Destination = "OSL",
                FromDate = DateTime.Now.AddDays(100),
                ToDate = DateTime.Now,
            };

            var hotelOptions = new Option[]
            {
                new()
                {
                    FlightCode = null,
                    FlightNumber = null,
                    ArrivalAirport = null,
                    DepartureAirport = null,
                    Price = 0,
                    HotelCode = "8626",
                    HotelId = 0,
                    HotelName = "Alexandar Square Boutique Hotel",
                    HotelDestinationCode = null,
                    HotelCity = null
                }
            };

            SearchRes searchRes = new()
            {
                Options = hotelOptions
            };

            _getSearchDataFromApiServiceMock.Setup(x => x.GetHotels(searchReq.Destination)).ReturnsAsync(hotelOptions);
            _searchRepositoryMock.Setup(x => x.StoreData(searchRes));

            var result = await _searchService.Search(searchReq);

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(searchRes.Options, result.Options);

            _getSearchDataFromApiServiceMock.Verify(x => x.GetHotels(searchReq.Destination), Times.Once);
        }
    }
}
