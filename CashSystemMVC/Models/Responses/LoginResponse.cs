using System;
using System.Collections.Generic;

namespace CashSystemMVC.Models.Responses
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string SessionToken { get; set; }
        public DateTime SessionExpiry { get; set; }

        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostAddress { get; set; }
        public string Postcode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
