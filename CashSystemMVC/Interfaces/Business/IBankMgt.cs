using System;
using System.Linq;
using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    /// <summary>
    /// The Business Interface that manages the Bank records
    /// </summary>

    public interface IBankMgt
    {
        Bank CreateBank(string name, string sortCode, float latitude, float longitude);
        Bank GetBank(int bankId);
        Bank UpdateBank(int bankId, string name, string sortCode, float latitude, float longitude);
        bool DeleteBank(int bankId);
    }
    /// <summary>
    /// The realisation of the Bank management interface
    /// </summary>
    public class BankMgt : IBankMgt
    {
        // The database
        private readonly DataContext _data;

        /// <summary>
        /// The constructor for the realisation of the Bank Manager
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        public BankMgt(DataContext data)
        {
            _data = data;
        }

        /// <summary>
        /// Creates a new bank
        /// </summary>
        /// <param name="name">The name of the Bank being used</param>
        /// <param name="latitude">The latitude of the Bank chosen</param>
        /// <param name="longitude">The longitude of the Bank chosen</param>
        /// <param name="sortCode">The sort code of the bank account provider</param>
        /// <param name="bankId">The ID of the Bank selected</param>
        /// <returns>The created account or null if the account could not be created</returns>
        public Bank CreateBank(int bankId, string name, string sortCode, float latitude, float longitude)
        {
            try
            {
                // Cannot find Bank without valid bank ID, name and sort code
                if (bankId == 0 ||
                    string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(sortCode))
                    return null;

                // Create Bank
                _data.Banks.Add(new Bank
                {
                    SortCode = sortCode,
                    Name = name,
                    Latitude = latitude,
                    Longitude = longitude
                });

                // return created Bank (null if account was not created)
                return _data.Banks.FirstOrDefault(b => b.SortCode == sortCode && b.Name == name && b.Latitude == latitude && b.Longitude == longitude);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// Queries the database for a Bank with matching ID
        /// </summary>
        /// <param name="bankId">The ID of the bank</param>
        /// <returns>The requested account or null if no account could be found</returns>
        public Bank GetBank(int bankId)
        {
            try
            {
                // Query for Bank, will be null if no Bank found
                return _data.Banks
                    .FirstOrDefault(b =>
                        b.BankId == bankId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Log the error exception data
                throw;
            }
        }

        /// <summary>
        /// Updates the balance of the account to the new balance if the userId, accountNumber and sortCode are valid
        /// </summary>
        /// <param name="bankId">The ID of the bank</param>
        /// <param name="name">The name of the Bank being used</param>
        /// <param name="sortCode">The sort code of the bank account provider</param>
        /// <param name="latitude">The latitude of the Bank chosen</param>
        /// <param name="longitude">The longitude of the Bank chosen</param> 
        /// <returns>The updated account or null if the account could not be found</returns>
        public Bank UpdateBank(int bankId, string name, string sortCode, float latitude, float longitude)
        {
            try
            {
                // Cannot find Bank without valid bank ID, name and sort code
                if (bankId == 0 ||
                    string.IsNullOrEmpty(name) ||
                    string.IsNullOrEmpty(sortCode))
                    return null;
                var bank = _data.Banks
                    .FirstOrDefault(b =>
                    b.BankId == bankId && b.Name == name && b.SortCode == sortCode);

                if (bank == null) return null; // No Bank found

                // Else update latitude and longitude
                bank.Latitude = latitude;
                _data.Banks.Update(bank);
                bank.Longitude = longitude;
                _data.Banks.Update(bank);

                // and return bank
                return bank;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Deletes the Bank ID from system/account
        /// </summary>
        /// <param name="bankId">The ID of the bank</param>
        /// <returns>A bool indicating if the delete was successful or not</returns>
        public bool DeleteBank(int bankId)
        {
            try
            {
                // Cannot find Bank without valid bank ID, name and sort code
                if (bankId == 0)
                    return false;

                // Get bank
                var bank = _data.Banks
                    .FirstOrDefault(b =>
                    b.BankId == bankId);

                // If bank doesn't exist, return false indicating failed delete
                if (bank == null) return false;

                // Else remove the bank
                _data.Banks.Remove(bank);

                // Query for bank again
                bank = _data.Banks
                    .FirstOrDefault(b =>
                    b.BankId == bankId);

                // Bank should be null so this will return true if delete is successful
                return bank == null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}