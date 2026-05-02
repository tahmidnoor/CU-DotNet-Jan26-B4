namespace LoanSphere_Frontend.Models
{
    public class LoanDetailsViewModel
    {
        public AuthenticatedUser CurrentUser { get; set; } = new();
        public LoanSummaryViewModel Loan { get; set; } = new();

        // 🔥 Derived Role
        public string ReviewerRole => CurrentUser?.Role ?? "";

        // 🔥 Can Review Logic (Admin / Manager)
        public bool CanReview
        {
            get
            {
                // 🔴 If anyone rejected → no review allowed
                if (Loan.AdminReviewStatus == "Rejected" ||
                    Loan.ManagerReviewStatus == "Rejected")
                {
                    return false;
                }

                // ✅ Admin can review
                if (ReviewerRole == "Admin" && Loan.AdminReviewStatus == "Pending")
                {
                    return true;
                }

                // ✅ Manager can review
                if (ReviewerRole == "Manager" && Loan.ManagerReviewStatus == "Pending")
                {
                    return true;
                }

                return false;
            }
        }

        // 🔥 EMI Schedule Visibility
        public bool CanSeePaymentSchedule => Loan.IsFullyApproved;

        // 🔥 NEW: Show Profile ONLY for Admin / Manager
        public bool CanViewProfile =>
            ReviewerRole == "Admin" || ReviewerRole == "Manager";

        // 🔥 Applicant Profile
        public ProfileViewModel? Profile { get; set; }
    }
}