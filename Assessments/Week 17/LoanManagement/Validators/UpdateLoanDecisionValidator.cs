using FluentValidation;
using LoanManagement.DTOs;

namespace LoanManagement.Validators
{
    public class UpdateLoanDecisionValidator : AbstractValidator<UpdateLoanDecisionDto>
    {
        public UpdateLoanDecisionValidator()
        {
            RuleFor(x => x.ReviewerRole)
                .Must(x => x == "Admin" || x == "Manager")
                .WithMessage("Only Admin or Manager allowed");

            RuleFor(x => x.Status)
                .Must(x => x == "Approved" || x == "Rejected")
                .WithMessage("Status must be Approved or Rejected");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .When(x => x.Status == "Rejected")
                .WithMessage("Reason required when rejecting");
        }
    }
}