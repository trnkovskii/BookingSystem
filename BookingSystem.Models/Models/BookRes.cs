namespace BookingSystem.Models.ViewModels
{
    public class BookRes
    {
        public string BookingCode { get; set; }
        public DateTime BookingTime { get; set; }
        public int SleepTime { get; set; }
        public bool IsFlightBooked { get; set; } = false;
        public bool IsHotelBooked { get; set; } = false;
        public bool IsLastMinuteReservation { get; set; } = false;
    }
}
