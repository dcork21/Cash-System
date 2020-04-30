using System;
using System.Linq;
using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.System
{
    /// <summary>
    /// The service interface which manages secure withdrawals
    /// </summary>
    public interface IRequestWithdraw
    {
        string CreateWithdraw(string sessionToken, int accountId, int amount);
        Withdrawal VerifyWithdraw(int accountId, string withdrawalToken);
        bool ConfirmWithdraw(int accountId, string withdrawalToken);

    }
    /// <summary>
    /// The realization of the secure withdrawal request service
    /// </summary>
    public class RequestWithdraw : IRequestWithdraw
    {
        private readonly ICrypto _crypto;
        private readonly IUdpClient _udpClient;

        // The database
        private readonly DataContext _data;

        /// <summary>
        ///     The constructor for the realization of the secure withdrawal request service
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        /// <param name="crypto">The Argon2 crypto implementation</param>
        public RequestWithdraw(DataContext data, ICrypto crypto, IUdpClient udpClient)
        {
            _data = data;
            _crypto = crypto;
            _udpClient = udpClient;
        }

        /// <summary>
        /// Creates a new withdrawal request against an account
        /// </summary>
        /// <param name="sessionToken">Used to authenticate the withdrawal request</param>
        /// <param name="accountId">The account the withdrawal is made against</param>
        /// <param name="amount">The amount in £GBP to be withdrawn</param>
        /// <returns></returns>
        public string CreateWithdraw(string sessionToken, int accountId, int amount)
        {

            // First get the account the request is to be made against.
            var account = _data.Accounts.FirstOrDefault(a => a.AccountId == accountId);

            // Return null if no account found
            if (account == null) return null;

            // Get the user which owns the account
            User user = _data.Users.FirstOrDefault(u => u.UserId == account.UserId);

            // If user is not found, the user session cannot be authenticated or the user session has expired, return null
            if (user == null || !user.SessionToken.Equals(sessionToken) || DateTime.Now.CompareTo(user.SessionExpiry) > 0) return null;


            // Otherwise, create the public token to be associated with the withdrawal.
            var publicToken = Guid.NewGuid().ToString("N");

            // And the secret, e.g 01-23-45:012345678
            var secret = account.AccountNumber + ":" + account.SortCode;


            // The create the withdrawal
            var withdrawal = new Withdrawal
            {
                AccountId = accountId,
                Amount = amount,
                ExpiryDate = DateTime.Now.AddMinutes(20),

                // The verification hash is the public token encrypted with the account number and sort code as the secret
                VerificationHash = _crypto.Encrypt(publicToken, secret, "WITHDRAWAL")
            };

            _data.Withdrawals.Add(withdrawal);
            _data.SaveChanges();

            return publicToken;
        }

        /// <summary>
        /// Authenticates a withdrawal request using the provided withdrawalToken
        /// </summary>
        /// <param name="accountId">The account which requested the withdrawal</param>
        /// <param name="withdrawalToken">The token used to verify the withdrawal request</param>
        /// <returns>A withdrawal if authentication is passed, otherwise null</returns>
        public Withdrawal VerifyWithdraw(int accountId, string withdrawalToken)
        {
            try
            {
                // First get the account the request is to be made against.
                var account = _data.Accounts.FirstOrDefault(a => a.AccountId == accountId);

                // Return null if no account found
                if (account == null) return null;

                // And the secret, e.g 01-23-45:012345678
                var secret = account.AccountNumber + ":" + account.SortCode;

                // Search database for any withdrawal owned by this account
                var withdrawals = _data.Withdrawals.Where(w => w.AccountId == accountId).ToList();


                // Now check all associated withdrawal to see if any match the withdrawalToken
                var withdrawal = withdrawals.FirstOrDefault(w =>
                    _crypto.Decrypt(w.VerificationHash, withdrawalToken, secret, "WITHDRAWAL"));

                // If none were found, return null
                if (withdrawal == null) return null;


                // If withdrawal has been found and has not expired, return withdrawal
                if (DateTime.Now.CompareTo(withdrawal.ExpiryDate) < 0) return withdrawal;


                // Finally clean up by removing any expired withdrawals
                _data.Withdrawals.RemoveRange(withdrawals.Where(w => DateTime.Now.CompareTo(withdrawal.ExpiryDate) > 0));
                _data.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// Confirms the withdrawal has been consumed
        /// </summary>
        /// <param name="accountId">The account which made the withdrawal</param>
        /// <param name="withdrawalToken">The token used to verify the withdrawal request</param>
        /// <returns></returns>
        public bool ConfirmWithdraw(int accountId, string withdrawalToken)
        {
            try
            {
                // First get the withdrawal by verifying it
                Withdrawal withdrawal = VerifyWithdraw(accountId, withdrawalToken);

                // If no withdrawal found, cannot confirm
                if (withdrawal == null) return false;


                _udpClient.SendMessageToAtm(6556, "Dispensing " + withdrawal.Amount + "pounds ...");

                // Finally clean up by deleting the completed withdrawal
                _data.Withdrawals.Remove(withdrawal);
                _data.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}