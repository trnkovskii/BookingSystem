using BookingSystem.Models.ViewModels;
using FluentValidation;

namespace BookingSystem.Models.FluentValidation
{
    public class BookReqValidator : AbstractValidator<BookReq>
    {
        public BookReqValidator()
        {
            RuleFor(x => x.OptionCode).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Option code is required.");
            RuleFor(x => x.SearchReq).NotNull().NotEmpty().WithMessage("Search request is required.");
        }
    }
}
