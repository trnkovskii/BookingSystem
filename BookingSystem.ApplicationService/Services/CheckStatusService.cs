using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.ApplicationService.Services
{
    public class CheckStatusService : ICheckStatusService
    {
        private readonly IBookRepository _bookRepository;
        public CheckStatusService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public CheckStatusRes CheckStatus(CheckStatusReq checkStatusReq)
        {
            var bookingRes = _bookRepository.GetByBookingCode(checkStatusReq.BookingCode);
            var checkStatusResult = new CheckStatusRes();

            if (bookingRes == null)
            {
                checkStatusResult.Status = Common.Enums.BookingStatusEnum.Failed;
                return checkStatusResult;
            }

            var expectedTimeToComplete = bookingRes.BookingTime.AddSeconds(bookingRes.SleepTime);

            if (DateTime.Now < expectedTimeToComplete)
            {
                checkStatusResult.Status = Common.Enums.BookingStatusEnum.Pending;
                return checkStatusResult;
            }
            else
            {
                if (bookingRes.IsHotelBooked || bookingRes.IsFlightBooked)
                {
                    checkStatusResult.Status = Common.Enums.BookingStatusEnum.Success;
                    return checkStatusResult;
                }
                else
                {
                    checkStatusResult.Status = Common.Enums.BookingStatusEnum.Failed;
                    return checkStatusResult;
                }
            }
        }

    }
}
