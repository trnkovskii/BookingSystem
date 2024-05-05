using BookingSystem.ApplicationService.Services;

namespace BookingSystem.Tests.ApplicationService
{
    [TestClass]
    public class RandomGeneratorServiceTests
    {
        private RandomGeneratorService _randomGeneratorService;

        [TestInitialize]
        public void RandomGeneratorServiceTestsInitialize()
        {
            _randomGeneratorService = new RandomGeneratorService();
        }

        [TestMethod]
        public void GenerateUniqueCodeReturnValid()
        {
            string code = _randomGeneratorService.GenerateUniqueCode();

            Assert.IsNotNull(code);
            Assert.AreEqual(6, code.Length);
            Assert.IsTrue(code.All(c => char.IsLetterOrDigit(c)));
        }

        [TestMethod]
        public void GenerateTimeBetween30and60ReturnValid()
        {
            int time = _randomGeneratorService.GenerateRandomTimeBetween30And60();

            Assert.IsTrue(time >= 30 && time < 60);
        }
    }
}
