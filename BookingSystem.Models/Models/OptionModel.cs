namespace BookingSystem.Models.ViewModels
{
    public class Option
    {
        public string OptionCode { get; set; }
        public string FlightCode { get; set; }
        public string FlightNumber { get; set; }
        public string ArrivalAirport { get; set; }
        public string DepartureAirport { get; set; }
        public double Price { get; set; }
        public string HotelCode { get; set; }
        public long HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelDestinationCode { get; set; }
        public string HotelCity { get; set; }
    }
}
