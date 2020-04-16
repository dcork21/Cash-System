using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IBankMgt
    {
        Bank CreateBank();
        Bank GetBank();
        Bank UpdateBank();
        bool DeleteBank();
    }
}