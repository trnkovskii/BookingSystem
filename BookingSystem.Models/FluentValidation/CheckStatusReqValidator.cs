using BookingSystem.Models.ViewModels;
using FluentValidation;

namespace BookingSystem.Models.FluentValidation
{
    public class CheckStatusReqValidator : AbstractValidator<CheckStatusReq>
    {
        public CheckStatusReqValidator()
        {
            RuleFor(x => x.BookingCode).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Booking code is required.");
        }
    }
}
