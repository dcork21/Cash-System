using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("Account")]
    public class Account
    {
        #region DatabaseProperties
        public int AccountId { get; set; }
        public int BankId { get; set; }
        public int UserId { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public float Balance { get; set; }
        #endregion

        #region RelationalData
        [ForeignKey("AccountId")] public List<Withdrawal> Withdrawals { get; set; }
        #endregion
    }
}
