using CashSystemMVC.Models;
using System;
using System.Linq;
using NotImplementedException = System.NotImplementedException;

namespace CashSystemMVC.Interfaces.Business
{
    /// <summary>
    /// The Business Interface which manages the ATM records
    /// </summary>
    public interface IAtmMgt
    {
        Atm CreateAtm(int bankId, float latitude, float longitude);
        Atm GetAtm(int atmId);
        Atm UpdateAtm(int atmId, float latitude, float longitude);
        bool DeleteAtm(int atmId);
    }

    /// <summary>
    /// The realization of the ATM management interface
    /// </summary>
    public class AtmMgt : IAtmMgt
    {
        // The database
        private readonly DataContext _data;

        /// <summary>
        /// The constructor for the realisation of the ATM Manager
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        public AtmMgt(DataContext data)
        {
            _data = data;
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="bankId">The ID of the Bank selected</param>
        /// <param name="latitude">The latitude of the ATM chosen</param>
        /// <param name="longitude">The longitude of the ATM chosen</param>
        /// <returns>The created account or null if the account could not be created</returns>
        public Atm CreateAtm(int atmId, float latitude, float longitude)
        {
            try
            {
                // Cannot find ATM without valid ATM ID, latitude and longitude
                if (atmId == 0 ||
                    float.IsNegative(latitude) ||
                    float.IsNegative(longitude))
                    return null;

                // Create ATM
                _data.Atms.Add(new Atm
                {
                    AtmId = atmId,
                    Latitude = latitude,
                    Longitude = longitude
                });
                // return created ATM (will be null if account was not created for some reason)
                return _data.Atms.FirstOrDefault(x => x.Latitude == latitude && x.Longitude == longitude);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Log the error exception data
                throw;
            }
        }

        /// <summary>
        /// Queries the database for an ATM with a matching ID
        /// </summary>
        /// <param name="atmId">The ID of the ATM</param>
        /// <returns>The requested account or null if no account could be found</returns>
        public Atm GetAtm(int atmId)
        {
            try
            {
                // Query for Bank, will be null if no Bank found
                return _data.Atms
                    .FirstOrDefault(x =>
                        x.BankId == atmId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Log the error exception data
                throw;
            }
        }
    }

    /// <summary>
    /// Updates the ATM
    /// </summary>
    /// <param name = "atmId" > The ID of the ATM selected</param>
    /// <param name="latitude">The latitude of the ATM chosen</param>
    /// <param name="longitude">The longitude of the ATM chosen</param>
    /// <returns>The updated account or null if the account could not be found</returns>
    public Atm UpdateAtm(int atmId, float latitude, float longitude)
    {
        try
        {
            // Cannot find ATM without valid ATM ID, latitude and longitude
            if (atmId == 0 ||
                float.IsNegative(latitude) ||
                float.IsNegative(longitude))
                return null;

            var atm = _data.Atms
                .FirstOrDefault(x =>
                x.AtmId == atmId && x.Latitude == latitude && x.Longitude == longitude);

            if (atm == null) return null; // No Bank found

            // Else update latitude and longitude
            atm.Latitude = latitude;
            _data.Banks.Update(atm);
            atm.Longitude = longitude;
            _data.Banks.Update(atm);

            // and return bank
            return atm;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

        public bool DeleteAtm()
        {
            throw new NotImplementedException();
        }
    }
}