using BookingSystem.ApplicationService.Interfaces;

namespace BookingSystem.ApplicationService.Services
{
    public class RandomGeneratorService : IRandomGeneratorService
    {
        private readonly Random _random;

        public RandomGeneratorService()
        {
            _random = new Random();
        }

        public string GenerateUniqueCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] code = new char[6];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = chars[_random.Next(chars.Length)];
            }

            return new string(code);
        }

        public int GenerateRandomTimeBetween30And60()
        {
            return _random.Next(30, 60);
        }
    }
}
