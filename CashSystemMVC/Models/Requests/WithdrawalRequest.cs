namespace CashSystemMVC.Models.Requests
{
    public class WithdrawalRequest
    {
        public int AccountId { get; set; }
        public string SessionToken { get; set; }
        public int Amount { get; set; }
    }
}
