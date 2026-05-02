namespace LoanManagement.DTOs
{
    public class UpdateLoanDecisionDto
    {
        public string ReviewerRole { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }
}