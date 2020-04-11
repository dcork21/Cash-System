using System;
namespace CashSystemMVC.Models
{
    public class ATM_Cashpoint
    {
        public int AtmId { get; set; }
        public int BankId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
       
    }
}
