using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;

namespace BookingSystem.ApplicationService.Services
{
    public class CheckStatusService : ICheckStatusService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookingStatusDeterminer _bookingStatusDeterminer;
        public CheckStatusService(IBookRepository bookRepository, IBookingStatusDeterminer bookingStatusDeterminer)
        {
            _bookRepository = bookRepository;
            _bookingStatusDeterminer = bookingStatusDeterminer;
        }

        public CheckStatusRes CheckStatus(CheckStatusReq checkStatusReq)
        {
            var bookingRes = _bookRepository.GetByBookingCode(checkStatusReq.BookingCode);

            if (bookingRes == null)
            {
                return new CheckStatusRes { Status = Common.Enums.BookingStatusEnum.Failed };
            }

            return new CheckStatusRes { Status = _bookingStatusDeterminer.DetermineStatus(bookingRes) };
        }
    }
}
