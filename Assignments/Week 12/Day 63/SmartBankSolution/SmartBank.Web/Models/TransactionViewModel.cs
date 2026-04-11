namespace SmartBank.Web.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }   // Deposit / Withdraw
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}