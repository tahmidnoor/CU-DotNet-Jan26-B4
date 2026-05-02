using FluentValidation;
using LoanManagement.DTOs;

namespace LoanManagement.Validators
{
    public class CreateLoanValidator : AbstractValidator<CreateLoanDto>
    {
        public CreateLoanValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Loan amount must be greater than 0");

            RuleFor(x => x.LoanType)
                .NotEmpty()
                .Must(x => x.ToLower() == "education" || x.ToLower() == "house")
                .WithMessage("LoanType must be 'education' or 'house'");

            RuleFor(x => x.TermInMonths)
                .InclusiveBetween(6, 360)
                .WithMessage("Term must be between 6 and 360 months");

            RuleFor(x => x.Purpose)
                .MaximumLength(100);
        }
    }
}