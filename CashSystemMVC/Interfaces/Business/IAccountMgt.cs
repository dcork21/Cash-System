using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IAccountMgt
    {
        bool CreateAccount();
        Account GetAccount();
        bool UpdateAccount();
        bool DeleteAccount();
    }
}