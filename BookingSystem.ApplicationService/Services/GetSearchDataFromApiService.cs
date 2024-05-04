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

        public async Task<Option[]> GetHotels(string destinationCode)
        {
            string apiUrl = _configuration["HotelsAPI"];

            var urlBuilder = new StringBuilder(apiUrl);
            urlBuilder.Append(destinationCode);

            return await GetDataFromAPI(urlBuilder.ToString());
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

        private static async Task<Option[]> GetDataFromAPI(string apiUrl)
        {
            try
            {
                using HttpClient client = new();

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await DeserializeData(response);
                }

                return Array.Empty<Option>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<Option[]> DeserializeData(HttpResponseMessage response)
        {
            string responseData = await response.Content.ReadAsStringAsync();

            Option[] options = JsonConvert.DeserializeObject<Option[]>(responseData);

            return options;
        }
    }
}
