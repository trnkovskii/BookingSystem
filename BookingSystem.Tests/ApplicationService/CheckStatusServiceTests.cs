using BookingSystem.ApplicationService.Interfaces;
using BookingSystem.ApplicationService.Services;
using BookingSystem.Models.ViewModels;
using BookingSystem.Storage.Interfaces;
using Moq;

namespace BookingSystem.Tests.ApplicationService
{
    [TestClass]
    public class CheckStatusServiceTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IBookingStatusDeterminer> _bookingStatusDeterminerMock;
        private CheckStatusService _checkStatusService;

        [TestInitialize]
        public void CheckStatusServiceTestsInitialize()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookingStatusDeterminerMock = new Mock<IBookingStatusDeterminer>();
            _checkStatusService = new CheckStatusService(_bookRepositoryMock.Object, _bookingStatusDeterminerMock.Object);
        }

        [TestMethod]
        public void CheckStatusAndReturnFailed()
        {
            string bookingCode = "1";
            var checkStatusRequest = new CheckStatusReq()
            {
                BookingCode = bookingCode
            };

            _bookRepositoryMock.Setup(x => x.GetByBookingCode(bookingCode)).Returns(It.IsAny<BookRes>());

            var result = _checkStatusService.CheckStatus(checkStatusRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(Common.Enums.BookingStatusEnum.Failed, result.Status);

            _bookRepositoryMock.Verify(x => x.GetByBookingCode(bookingCode), Times.Once);
        }

        [TestMethod]
        public void CheckStatusAndReturnPending()
        {
            var bookingRes = new BookRes()
            {
                BookingCode = "1",
                BookingTime = DateTime.Now,
                IsFlightBooked = false,
                IsHotelBooked = false,
                IsLastMinuteReservation = false,
                SleepTime = 57
            };

            string bookingCode = "1";

            var checkStatusRequest = new CheckStatusReq()
            {
                BookingCode = bookingCode
            };

            _bookRepositoryMock.Setup(x => x.GetByBookingCode(bookingCode)).Returns(bookingRes);
            _bookingStatusDeterminerMock.Setup(x => x.DetermineStatus(bookingRes)).Returns(Common.Enums.BookingStatusEnum.Pending);

            var result = _checkStatusService.CheckStatus(checkStatusRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(Common.Enums.BookingStatusEnum.Pending, result.Status);

            _bookRepositoryMock.Verify(x => x.GetByBookingCode(bookingCode), Times.Once);
            _bookingStatusDeterminerMock.Verify(x => x.DetermineStatus(bookingRes), Times.Once);
        }

        [TestMethod]
        public void CheckStatusAndReturnSuccess()
        {
            var bookingRes = new BookRes()
            {
                BookingCode = "1",
                BookingTime = DateTime.Now,
                IsFlightBooked = false,
                IsHotelBooked = false,
                IsLastMinuteReservation = false,
                SleepTime = 57
            };

            string bookingCode = "1";

            var checkStatusRequest = new CheckStatusReq()
            {
                BookingCode = bookingCode
            };

            _bookRepositoryMock.Setup(x => x.GetByBookingCode(bookingCode)).Returns(bookingRes);
            _bookingStatusDeterminerMock.Setup(x => x.DetermineStatus(bookingRes)).Returns(Common.Enums.BookingStatusEnum.Success);

            var result = _checkStatusService.CheckStatus(checkStatusRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(Common.Enums.BookingStatusEnum.Success, result.Status);

            _bookRepositoryMock.Verify(x => x.GetByBookingCode(bookingCode), Times.Once);
            _bookingStatusDeterminerMock.Verify(x => x.DetermineStatus(bookingRes), Times.Once);
        }
    }
}
