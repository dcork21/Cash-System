using System;

namespace CashSystemMVC.Models.Responses
{
    public class WithdrawalResponse
    {
        public int AccountId { get; set; }
        public string WithdrawalToken { get; set; }
        public DateTime Expiry { get; set; }
    }
}