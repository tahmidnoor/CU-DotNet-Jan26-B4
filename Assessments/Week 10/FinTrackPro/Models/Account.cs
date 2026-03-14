using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FinTrackPro.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string Type { get; set; }
        [ValidateNever]
        public List<Transaction> Transactions { get; set; }
    }
}
