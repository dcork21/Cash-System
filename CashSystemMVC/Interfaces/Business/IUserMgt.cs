using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IUserMgt
    {
        bool CreateUser();
        User GetUser();
        bool UpdateUser();
        bool DeleteUser();
    }
}