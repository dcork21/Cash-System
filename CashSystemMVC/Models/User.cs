using System;
namespace CashSystemMVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int IdentityId { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SortCode { get; set; }
    
    }
}
