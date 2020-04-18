using CashSystemMVC.Models;
using System;
using System.Linq;

namespace CashSystemMVC.Interfaces.Business
{
    /// <summary>
    /// The Business Interface which manages the ATM records
    /// </summary>
    public interface IAtmMgt
    {
        Atm CreateAtm(float latitude, float longitude);
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
        /// The constructor for the realization of the ATM Manager
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        public AtmMgt(DataContext data)
        {
            _data = data;
        }

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="latitude">The latitude of the ATM chosen</param>
        /// <param name="longitude">The longitude of the ATM chosen</param>
        /// <returns>The created account or null if the account could not be created</returns>
        public Atm CreateAtm(float latitude, float longitude)
        {
            try
            {
                // Cannot find ATM without valid ATM ID, latitude and longitude
                if (float.IsNegative(latitude) ||
                    float.IsNegative(longitude))
                    return null;

                // Create ATM
                _data.Atms.Add(new Atm
                {
                    Latitude = latitude,
                    Longitude = longitude
                });
                _data.SaveChanges();

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
                // Query for atm, will be null if no atm found
                return _data.Atms.FirstOrDefault(x => x.AtmId == atmId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Log the error exception data
                throw;
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
                var atm = _data.Atms.FirstOrDefault(x => x.AtmId == atmId);

                if (atm == null) return null; // No atm found

                // Else update latitude and longitude
                atm.Longitude = longitude;
                atm.Latitude = latitude;
                _data.Atms.Update(atm);
                _data.SaveChanges();

                // and return atm
                return atm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool DeleteAtm(int atmId)
        {
            try
            {
                // Get Atm
                var atm = _data.Atms.FirstOrDefault(a => a.AtmId == atmId );


                // If ATM does not exist, return false indicating failed delete
                if (atm == null) return false;

                // Else remove the account
                _data.Atms.Remove(atm);
                _data.SaveChanges();

                // Account should be null so this will return true if delete successful.
                return _data.Atms.FirstOrDefault(a => a.AtmId == atmId) == null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}