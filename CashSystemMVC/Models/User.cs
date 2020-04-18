using System;
namespace CashSystemMVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int IdentityId { get; set; }
        public string SessionToken { get; set; }
        public DateTime SessionExpiry { get; set; }
    }
}
