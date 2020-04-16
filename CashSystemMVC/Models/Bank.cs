using System;
namespace CashSystemMVC.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string Name { get; set; }
        public string SortCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
