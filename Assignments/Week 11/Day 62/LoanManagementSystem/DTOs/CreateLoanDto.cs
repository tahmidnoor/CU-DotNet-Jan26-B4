namespace LoanManagementSystem.DTOs
{
    public class CreateLoanDto
    {
        public string BorrowerName { get; set; }
        public decimal Amount { get; set; }
        public int LoanTermMonths { get; set; }
    }
}
