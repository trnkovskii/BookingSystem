using BookingSystem.Models.ViewModels;

namespace BookingSystem.Storage.Interfaces
{
    public interface ICheckStatusRepository : IInMemoryRepository<CheckStatusRes>
    {
    }
}