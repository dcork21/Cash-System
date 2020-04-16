using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IAtmMgt
    {
        Atm CreateAtm();
        Atm GetAtm();
        Atm UpdateAtm();
        bool DeleteAtm();
    }
}
