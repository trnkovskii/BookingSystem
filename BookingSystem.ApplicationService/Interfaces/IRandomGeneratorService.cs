namespace BookingSystem.ApplicationService.Interfaces
{
    public interface IRandomGeneratorService
    {
        string GenerateUniqueCode();
        int GenerateRandomTimeBetween30And60();
    }
}