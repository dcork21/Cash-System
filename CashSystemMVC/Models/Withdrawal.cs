using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("Withdrawal")]

    public class Withdrawal
    {
        public int WithdrawalId { get; set; }
        public int AccountId { get; set; }
        public string VerificationHash { get; set; }
        public float Amount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
