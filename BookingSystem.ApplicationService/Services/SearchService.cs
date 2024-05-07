using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.ApplicationService.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        private readonly IGetSearchDataFromApiService _getSearchDataFromApiService;
        public SearchService(ISearchRepository searchRepository, IGetSearchDataFromApiService getSearchDataFromApiService)
        {
            _searchRepository = searchRepository;
            _getSearchDataFromApiService = getSearchDataFromApiService;
        }

        public async Task<SearchRes> Search(SearchReq searchReq)
        {
            try
            {
                if (string.IsNullOrEmpty(searchReq.DepartureAirport))
                {
                    var result = await SearchHotels(searchReq).ConfigureAwait(false);
                    StoreSearchResults(result);
                    return result;
                }
                else
                {
                    var result = await SearchFlightsAndHotels(searchReq);
                    StoreSearchResults(result);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<SearchRes> SearchFlightsAndHotels(SearchReq searchReq)
        {
            return await GetFlightsAndHotelsAsync(searchReq.Destination, searchReq.DepartureAirport);
        }

        private async Task<SearchRes> SearchHotels(SearchReq searchReq)
        {
            if (IsLastMinuteHotelsSearch(searchReq.FromDate))
            {
                return new SearchRes { Options = await _getSearchDataFromApiService.GetHotels(searchReq.Destination, true).ConfigureAwait(false) };
            }
            else
            {
                return new SearchRes { Options = await _getSearchDataFromApiService.GetHotels(searchReq.Destination).ConfigureAwait(false) };
            }
        }

        private async Task<SearchRes> GetFlightsAndHotelsAsync(string destinationCode, string departureAirport)
        {
            string arrivalAirport = destinationCode;

            var flights = await _getSearchDataFromApiService.GetFlights(departureAirport, arrivalAirport);
            var hotels = await _getSearchDataFromApiService.GetHotels(destinationCode);

            var options = flights.Concat(hotels).ToArray();

            return new SearchRes { Options = options };
        }

        private void StoreSearchResults(SearchRes searchRes)
        {
            _searchRepository.StoreData(searchRes);
        }

        private static bool IsLastMinuteHotelsSearch(DateTime fromDate)
        {
            var in45Days = DateTime.Today.AddDays(45);
            return fromDate <= in45Days;
        }
    }
}
