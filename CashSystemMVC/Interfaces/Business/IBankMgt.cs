using System;
using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IBankMgt
    {
        Bank CreateBank();
        Bank GetBank();
        Bank UpdateBank();
        bool DeleteBank();


        public class BankMgt : IBankMgt
        {
            // The database
            private readonly DataContext _data;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="data">The database context dependency injected via StartUp</param>
            public BankMgt(DataContext data)
            {
                _data = data;
            }

            public Bank CreateBank()
            {
                throw new NotImplementedException();
            }

            public Bank GetBank()
            {
                throw new NotImplementedException();
            }

            public Bank UpdateBank()
            {
                throw new NotImplementedException();
            }

            public bool DeleteBank()
            {
                throw new NotImplementedException();
            }
        }
}