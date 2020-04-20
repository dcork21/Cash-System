using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("Bank")]
    public class Bank
    {
        #region DatabaseProperties
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string SortCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion

        #region RelationalData
        [ForeignKey("BankId")] public List<Atm> Atms { get; set; }
        #endregion
    }
}
