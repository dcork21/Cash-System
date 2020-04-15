using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IBankMgt
    {
        bool CreateBank();
        Bank GetBank();
        bool UpdateBank();
        bool DeleteBank();
    }
}