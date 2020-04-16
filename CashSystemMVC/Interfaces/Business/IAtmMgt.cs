using CashSystemMVC.Models;
using NotImplementedException = System.NotImplementedException;

namespace CashSystemMVC.Interfaces.Business
{
    public interface IAtmMgt
    {
        Atm CreateAtm();
        Atm GetAtm();
        Atm UpdateAtm();
        bool DeleteAtm();
    }


    public class AtmMgt : IAtmMgt
    {
        // The database
        private readonly DataContext _data;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        public AtmMgt(DataContext data)
        {
            _data = data;
        }

        public Atm CreateAtm()
        {
            throw new NotImplementedException();
        }

        public Atm GetAtm()
        {
            throw new NotImplementedException();
        }

        public Atm UpdateAtm()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAtm()
        {
            throw new NotImplementedException();
        }
    }
}
