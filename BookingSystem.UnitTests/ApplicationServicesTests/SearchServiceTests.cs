using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookingSystem.UnitTests.ApplicationServicesTests
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
        public async Task SearchMethodAndReturnHotel()
        {
            SearchReq searchReq = new()
            {
                DepartureAirport = "SKP",
                Destination = "OSL",
                FromDate = DateTime.Now,
                ToDate = DateTime.Now,
            };

            var options = new Option[]
            {
                new()
                {
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
                },
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
                Options = options
            };

            _getSearchDataFromApiServiceMock.Setup(x => x.GetHotels(searchReq.Destination)).ReturnsAsync(options);
            _searchRepositoryMock.Setup(x => x.StoreData(searchRes));

            var result = await _searchService.Search(searchReq);
            Assert.IsNotNull(result);
        }
    }
}
