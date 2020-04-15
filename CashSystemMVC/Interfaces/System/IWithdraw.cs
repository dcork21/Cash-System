using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.System
{
    public interface IWithdraw
    {
        bool CreateWithdraw();
        bool VerifyWithdraw();
        bool ConfirmWithdraw();
    }
}
