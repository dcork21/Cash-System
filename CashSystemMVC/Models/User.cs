using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("User")]
    public class User
    {
        #region DatabaseProperties
        public int UserId { get; set; }
        public int IdentityId { get; set; }
        public string SessionToken { get; set; }
        public DateTime SessionExpiry { get; set; }
        #endregion

        #region RelationalData
        [ForeignKey("IdentityId")] public Identity UserIdentity { get; set; }
        [ForeignKey("UserId")] public List<Account> Accounts { get; set; }
        #endregion
    }
}
