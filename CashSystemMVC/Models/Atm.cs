using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("Atm")]
    public class Atm
    {
        public int AtmId { get; set; }
        public int BankId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
