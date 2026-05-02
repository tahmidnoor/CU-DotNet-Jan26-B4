namespace LoanManagement.DTOs
{
    public class LoanDto
    {
        public string UserId { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string LoanType { get; set; } = string.Empty;
        public int TermInMonths { get; set; }
        public string Purpose { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public string DocsVerified { get; set; } = string.Empty;

        public string AdminReviewStatus { get; set; } = string.Empty;
        public string? AdminReviewReason { get; set; }
        public DateTime? AdminReviewedAt { get; set; }

        public string ManagerReviewStatus { get; set; } = string.Empty;
        public string? ManagerReviewReason { get; set; }
        public DateTime? ManagerReviewedAt { get; set; }

        public DateTime AppliedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}