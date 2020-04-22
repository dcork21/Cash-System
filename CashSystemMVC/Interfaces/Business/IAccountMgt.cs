using System;
using System.Linq;
using CashSystemMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CashSystemMVC.Interfaces.Business
{
    /// <summary>
    ///     The Business Interface which manages the Account records
    /// </summary>
    public interface IAccountMgt
    {
        Account CreateAccount(string sessionToken, int userId, string accountNumber, string sortCode, float balance);
        Account GetAccount(int userId, string accountNumber, string sortCode);
        Account UpdateAccount(int userId, string accountNumber, string sortCode, float balance);
        bool DeleteAccount(int userId, string accountNumber, string sortCode);
    }

    /// <summary>
    ///     The realization of the Account manager interface
    /// </summary>
    public class AccountMgt : IAccountMgt
    {
        // The database
        private readonly DataContext _data;

        /// <summary>
        ///     The constructor for the realization of the Account Manager
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        public AccountMgt(DataContext data)
        {
            _data = data;
        }

        /// <summary>
        ///     Creates a new account
        /// </summary>
        /// <param name="sessionToken">Used to authenticate the request</param>
        /// <param name="userId">The ID of the User the account belongs to</param>
        /// <param name="accountNumber">The bank account number of the account</param>
        /// <param name="sortCode">The sort code of the bank account provider</param>
        /// <param name="balance">The current balance of the account in £GBP</param>
        /// <returns>The created account or null if the account could not be created</returns>
        public Account CreateAccount(string sessionToken, int userId, string accountNumber, string sortCode, float balance)
        {
            try
            {
                // Can't find account without valid user ID, sort code and account number
                if (userId == 0 ||
                    string.IsNullOrEmpty(accountNumber) ||
                    string.IsNullOrEmpty(sortCode))
                    return null;

                // Get the user which owns the account
                User user = _data.Users.FirstOrDefault(u => u.UserId == userId);

                
                // If user is not found, the user session cannot be authenticated or the user session has expired, return null
                if (user == null || !user.SessionToken.Equals(sessionToken) || DateTime.Now.CompareTo(user.SessionExpiry) > 0) return null;

                Bank bank = _data.Banks.FirstOrDefault(b => b.SortCode.Equals(sortCode));

                if (bank == null) return null;

                // Create account
                _data.Accounts.Add(new Account
                {
                    UserId = userId,
                    BankId = bank.BankId,
                    SortCode = sortCode,
                    AccountNumber = accountNumber,
                    Balance = balance
                });
                _data.SaveChanges();

                // return created account (will be null if account was not created for some reason)
                return _data.Accounts.Include(a => a.Bank).FirstOrDefault(a => a.AccountNumber == accountNumber && a.SortCode == sortCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Log the error exception data
                throw;
            }
        }

        /// <summary>
        ///     Queries the database for an account with a matching account number and sort code
        /// </summary>
        /// <param name="userId">The ID of the User the account belongs to</param>
        /// <param name="accountNumber">The bank account number of the account</param>
        /// <param name="sortCode">The sort code of the bank account provider</param>
        /// <returns>The requested account or null if no account could be found</returns>
        public Account GetAccount(int userId, string accountNumber, string sortCode)
        {
            try
            {
                // Query for account, will be null if no account found
                return _data.Accounts
                    .FirstOrDefault(a =>
                        a.UserId == userId && a.AccountNumber == accountNumber && a.SortCode == sortCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // Log the error exception data
                throw;
            }
        }

        /// <summary>
        ///     Updates the balance of the account to the new balance if the userId, accountNumber and sortCode are valid
        /// </summary>
        /// <param name="userId">The ID of the User the account belongs to</param>
        /// <param name="accountNumber">The bank account number of the account</param>
        /// <param name="sortCode">The sort code of the bank account provider</param>
        /// <param name="balance">The new balance of the account in £GBP</param>
        /// <returns>The updated account or null if the account could not be found</returns>
        public Account UpdateAccount(int userId, string accountNumber, string sortCode, float balance)
        {
            try
            {
                // Can't find account without valid sort code and account number
                if (userId == 0 ||
                    string.IsNullOrEmpty(accountNumber) ||
                    string.IsNullOrEmpty(sortCode))
                    return null;

                var account = _data.Accounts
                    .FirstOrDefault(a =>
                        a.UserId == userId && a.AccountNumber == accountNumber && a.SortCode == sortCode);

                if (account == null) return null; // No account found

                // Else update balance
                account.Balance = balance;
                _data.Accounts.Update(account);
                _data.SaveChanges();

                // and return account
                return account;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="userId">The ID of the User the account belongs to</param>
        /// <param name="accountNumber">The bank account number of the account</param>
        /// <param name="sortCode">The sort code of the bank account provider</param>
        /// <returns>A bool indicating if the delete was successful or not</returns>
        public bool DeleteAccount(int userId, string accountNumber, string sortCode)
        {
            try
            {
                // Can't find account without valid sort code and account number
                if (userId == 0 ||
                    string.IsNullOrEmpty(accountNumber) ||
                    string.IsNullOrEmpty(sortCode))
                    return false;

                // Get account
                var account = _data.Accounts
                    .FirstOrDefault(a =>
                        a.UserId == userId && a.AccountNumber == accountNumber && a.SortCode == sortCode);


                // If account does not exist, return false indicating failed delete
                if (account == null) return false;

                // Else remove the account
                _data.Accounts.Remove(account);
                _data.SaveChanges();


                // Account should be null so this will return true if delete successful.
                return _data.Accounts.FirstOrDefault(a => 
                           a.UserId == userId && 
                           a.AccountNumber == accountNumber 
                           && a.SortCode == sortCode) == null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}