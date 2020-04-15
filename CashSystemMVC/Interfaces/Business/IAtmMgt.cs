using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IAtmMgt
    {
        bool CreateAtm();
        Atm GetAtm();
        bool UpdateAtm();
        bool DeleteAtm();
    }
}
