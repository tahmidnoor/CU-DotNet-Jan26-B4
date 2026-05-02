namespace LoanManagement.DTOs
{
    public class CreateLoanDto
    {
        public string UserId { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string LoanType { get; set; } = string.Empty;
        public int TermInMonths { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string DocsVerified { get; set; } = string.Empty;
    }
}