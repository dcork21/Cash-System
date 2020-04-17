using System;
namespace CashSystemMVC.Models
{
    public class Withdrawal
    {
        public int WithdrawalId { get; set; }
        public int AccountId { get; set; }
        public float Amount { get; set; }
    }
}
