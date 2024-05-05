using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BookingSystem.ApplicationService.Services
{
    public class GetSearchDataFromApiService : IGetSearchDataFromApiService
    {
        private readonly IConfiguration _configuration;
        public GetSearchDataFromApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Option[]> GetHotels(string destinationCode, bool isLastMinute = false)
        {
            string apiUrl = _configuration["HotelsAPI"];

            var urlBuilder = new StringBuilder(apiUrl);
            urlBuilder.Append(destinationCode);

            return await GetDataFromAPI(urlBuilder.ToString(), isLastMinute);
        }

        public async Task<Option[]> GetFlights(string departureAirport, string arrivalAirport)
        {
            string apiUrl = _configuration["FlightsAPI"];

            var urlBuilder = new StringBuilder(apiUrl);
            urlBuilder.Append(departureAirport);
            urlBuilder.Append("&arrivalAirport=");
            urlBuilder.Append(arrivalAirport);

            return await GetDataFromAPI(urlBuilder.ToString());
        }

        private static async Task<Option[]> GetDataFromAPI(string apiUrl, bool isLastMinute = false)
        {
            try
            {
                using HttpClient client = new();

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await DeserializeData(response, isLastMinute);
                }

                return Array.Empty<Option>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<Option[]> DeserializeData(HttpResponseMessage response, bool isLastMinute = false)
        {
            string responseData = await response.Content.ReadAsStringAsync();

            Option[] options = JsonConvert.DeserializeObject<Option[]>(responseData);

            if (isLastMinute)
            {
                foreach (var item in options)
                {
                    item.IsLastMinuteReservation = true;
                }
            }

            return options;
        }
    }
}
