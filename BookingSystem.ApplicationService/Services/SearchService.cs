using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage;
using Newtonsoft.Json;

namespace BookingSystem.ApplicationService.Services
{
    public class SearchService : ISearchService
    {
        private readonly IInMemoryStorage<SearchRes> _inMemoryStorage;
        public SearchService(IInMemoryStorage<SearchRes> inMemoryStorage)
        {
            _inMemoryStorage = inMemoryStorage;
        }

        public async Task<SearchRes> Search(SearchReq searchReq)
        {
            var searchRes = new SearchRes();

            if (string.IsNullOrEmpty(searchReq.DepartureAirport))
            {
                if (IsLastMinuteHotelsSearch(searchReq.FromDate))
                {
                    searchRes = await GetHotelsAsync(searchReq, searchRes).ConfigureAwait(false);

                    _inMemoryStorage.StoreData(searchRes);

                    return searchRes;
                }
                else
                {
                    searchRes = await GetHotelsAsync(searchReq, searchRes).ConfigureAwait(false);

                    _inMemoryStorage.StoreData(searchRes);

                    return searchRes;
                }
            }
            else
            {
                var (flights, hotels) = await GetHotelsAndFlights(searchReq.Destination, searchReq.DepartureAirport);

                var options = flights.Concat(hotels).ToArray();

                searchRes.Options = options;

                _inMemoryStorage.StoreData(searchRes);

                return searchRes;
            }
        }

        private static async Task<SearchRes> GetHotelsAsync(SearchReq searchReq, SearchRes searchRes)
        {
            var hotels = await GetHotels(searchReq.Destination).ConfigureAwait(false);
            searchRes.Options = hotels;
            return searchRes;
        }

        private static async Task<Option[]> GetHotels(string destinationCode)
        {
            using HttpClient client = new();

            try
            {
                string apiUrl = $"https://tripx-test-functions.azurewebsites.net/api/SearchHotels?destinationCode={destinationCode}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                Option[] hotels = Array.Empty<Option>();

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    hotels = JsonConvert.DeserializeObject<Option[]>(responseData);
                }

                return hotels;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<Option[]> GetFlights(string departureAirport, string arrivalAirport)
        {
            using HttpClient client = new();

            try
            {
                string apiUrl = $"https://tripx-test-functions.azurewebsites.net/api/SearchFlights?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                Option[] flights = Array.Empty<Option>();

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    flights = JsonConvert.DeserializeObject<Option[]>(responseData);
                }

                return flights;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool IsLastMinuteHotelsSearch(DateTime fromDate)
        {
            var for45Days = DateTime.Today.AddDays(45);
            return fromDate <= for45Days;
        }

        private static async Task<(Option[] Flights, Option[] Hotels)> GetHotelsAndFlights(string destinationCode, string departureAirport)
        {
            string arrivalAirport = destinationCode;

            var hotels = await GetHotels(destinationCode);

            var flights = await GetFlights(departureAirport, arrivalAirport);

            return (flights, hotels);
        }
    }
}
