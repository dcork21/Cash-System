using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IUserMgt
    {
        User CreateUser();
        User GetUser();
        User UpdateUser();
        bool DeleteUser();
    }
}