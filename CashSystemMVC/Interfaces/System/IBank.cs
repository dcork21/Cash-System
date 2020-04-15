using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashSystemMVC.Interfaces.System
{
    interface IBank
    {
        float GetBalance();
        bool ReserveCredit();
    }
}
