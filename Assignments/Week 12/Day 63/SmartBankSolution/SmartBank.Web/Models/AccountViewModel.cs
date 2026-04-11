using System.Collections.Generic;

namespace SmartBank.Web.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }
            = new List<TransactionViewModel>();
    }
}