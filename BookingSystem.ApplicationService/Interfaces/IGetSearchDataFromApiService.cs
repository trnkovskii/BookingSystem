using BookingSystem.Models.ViewModels;

namespace BookingSystem.ApplicationService.Interfaces
{
    public interface IGetSearchDataFromApiService
    {
        Task<Option[]> GetHotels(string destinationCode, bool isLastMinute = false);
        Task<Option[]> GetFlights(string departureAirport, string arrivalAirport);
    }
}