using BookingSystem.Models.ViewModels;

namespace BookingSystem.ApplicationService.Interfaces
{
    public interface ICheckStatusService
    {
        CheckStatusRes CheckStatus(CheckStatusReq checkStatusReq);
    }
}