using BookingSystem.Models.ViewModels;

namespace BookingSystem.Storage.Interfaces
{
    internal interface ICheckStatusRepository : IInMemoryRepository<CheckStatusRes>
    {
    }
}