namespace CashSystemMVC.Interfaces.System
{
    interface IBank
    {
        float GetBalance();
        bool ReserveCredit();
    }
}
