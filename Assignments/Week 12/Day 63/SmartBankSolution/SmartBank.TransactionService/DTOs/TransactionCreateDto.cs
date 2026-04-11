namespace SmartBank.TransactionService.DTOs
{
    public class TransactionCreateDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}