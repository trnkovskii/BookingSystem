using BookingSystem.Models.ViewModels;
using FluentValidation;

namespace BookingSystem.Models.FluentValidation
{
    public class SearchReqValidator : AbstractValidator<SearchReq>
    {
        public SearchReqValidator()
        {
            RuleFor(x => x.Destination).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Destination is required.");
            RuleFor(x => x.FromDate).NotNull().NotEmpty().WithMessage("From date is required.");
            RuleFor(x => x.ToDate).NotNull().NotEmpty().WithMessage("To date is required.");
        }
    }
}
