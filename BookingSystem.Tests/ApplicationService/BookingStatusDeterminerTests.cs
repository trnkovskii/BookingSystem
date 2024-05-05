using BookingSystem.ApplicationService.Services;
using BookingSystem.Common.Enums;
using BookingSystem.Models.ViewModels;

namespace BookingSystem.Tests.ApplicationService
{
    [TestClass]
    public class BookingStatusDeterminerTests
    {
        private BookingStatusDeterminer _statusDeterminer;

        [TestInitialize]
        public void BookingStatusDeterminerTestsInitialize()
        {
            _statusDeterminer = new BookingStatusDeterminer();
        }

        [TestMethod]
        public void DetermineStatusReturnsPending()
        {
            var bookingTime = DateTime.Now;
            var sleepTime = 60;
            var bookingRes = new BookRes
            {
                BookingTime = bookingTime,
                SleepTime = sleepTime
            };

            var result = _statusDeterminer.DetermineStatus(bookingRes);

            Assert.AreEqual(BookingStatusEnum.Pending, result);
        }

        [TestMethod]
        public void DetermineStatusReturnsSuccess()
        {
            var bookingTime = DateTime.Now.AddMinutes(-2);
            var sleepTime = 50;
            var bookingRes = new BookRes
            {
                BookingTime = bookingTime,
                SleepTime = sleepTime,
                IsHotelBooked = true,
                IsFlightBooked = true,
                IsLastMinuteReservation = false
            };

            var result = _statusDeterminer.DetermineStatus(bookingRes);

            Assert.AreEqual(BookingStatusEnum.Success, result);
        }

        [TestMethod]
        public void DetermineStatusReturnsFailed()
        {
            var bookingTime = DateTime.Now.AddMinutes(-2);
            var sleepTime = 35;
            var bookingRes = new BookRes
            {
                BookingTime = bookingTime,
                SleepTime = sleepTime,
                IsHotelBooked = true,
                IsFlightBooked = true,
                IsLastMinuteReservation = true
            };

            var result = _statusDeterminer.DetermineStatus(bookingRes);

            Assert.AreEqual(BookingStatusEnum.Failed, result);
        }
    }
}
