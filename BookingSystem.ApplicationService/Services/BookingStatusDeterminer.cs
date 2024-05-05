using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Common.Enums;
using BookingSystem.Models.ViewModels;

namespace BookingSystem.ApplicationService.Services
{
    public class BookingStatusDeterminer : IBookingStatusDeterminer
    {
        public BookingStatusEnum DetermineStatus(BookRes bookingRes)
        {
            var expectedTimeToComplete = bookingRes.BookingTime.AddSeconds(bookingRes.SleepTime);

            if (DateTime.Now < expectedTimeToComplete)
            {
                return BookingStatusEnum.Pending;
            }
            else
            {
                if (bookingRes.IsHotelBooked || bookingRes.IsFlightBooked)
                {
                    return BookingStatusEnum.Success;
                }
                else
                {
                    return BookingStatusEnum.Failed;
                }
            }
        }
    }
}
