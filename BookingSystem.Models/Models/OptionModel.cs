namespace BookingSystem.Models.ViewModels
{
    public class Option
    {
        private static readonly Random _random = new();

        public Option()
        {
            OptionCode = GenerateOptionCode();
        }

        public string OptionCode { get; private set; }
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

        private static string GenerateOptionCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] code = new char[6];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chars[_random.Next(chars.Length)];
            }

            return new string(code);
        }
    }
}
