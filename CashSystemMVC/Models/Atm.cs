using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("Atm")]
    public class Atm
    {
        public int AtmId { get; set; }
        [ForeignKey("BankId")] public int BankId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
