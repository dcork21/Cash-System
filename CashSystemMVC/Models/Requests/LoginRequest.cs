namespace CashSystemMVC.Models.Requests
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LoginToken { get; set; }
    }
}
