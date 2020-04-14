using System;

namespace CashSystemMVC.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public float Balance { get; set; }
     
    }
}
