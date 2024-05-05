using BookingSystem.Common.Enums;
using BookingSystem.Models.ViewModels;

namespace BookingSystem.ApplicationService.Interfaces
{
    public interface IBookingStatusDeterminer
    {
        BookingStatusEnum DetermineStatus(BookRes booking);
    }
}