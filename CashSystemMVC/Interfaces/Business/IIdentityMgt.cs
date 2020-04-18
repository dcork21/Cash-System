using System;
using System.Linq;
using CashSystemMVC.Interfaces.System;
using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    /// <summary>
    ///     The Business Interface which manages the Identity records
    /// </summary>
    public interface IIdentityMgt
    {
        Identity CreateIdentity(string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email);

        Identity GetIdentity(string userName,
            string password);

        Identity UpdateIdentity(string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email);

        bool DeleteIdentity(Identity identity);
    }

    /// <summary>
    ///     The realization of the Identity manager interface
    /// </summary>
    public class IdentityMgt : IIdentityMgt
    {
        // The database
        private readonly DataContext _data;
        private readonly ICrypto _crypto;

        /// <summary>
        ///     The constructor for the realization of the Identity Manager
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        /// <param name="crypto">The Argon2 crypto implementation</param>
        public IdentityMgt(DataContext data, ICrypto crypto)
        {
            _data = data;
            _crypto = crypto;
        }

        /// <summary>
        /// Creates a new Identity record
        /// </summary>
        /// <param name="userName">The handle used to login</param>
        /// <param name="password">The password used to login</param>
        /// <param name="firstName">The first name of the identity</param>
        /// <param name="lastName">The last name of the identity</param>
        /// <param name="address">The address of the identity</param>
        /// <param name="postcode">The postcode of the identity</param>
        /// <param name="mobile">The mobile phone number of the identity</param>
        /// <param name="email">The email address of the identity</param>
        /// <returns>The created Identity</returns>
        public Identity CreateIdentity(string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email)
        {

            // Create a new identity object
            var identity = new Identity(userName,
                firstName,
                lastName,
                address,
                postcode,
                mobile,
                email)
            {
                PasswordHash = _crypto.Encrypt(password, userName, "PASSWORD") // Securely store the password as a hash
            };

            _data.Identities.Add(identity); // Store the record
            _data.SaveChanges(); // Write to database
            return identity;
        }

        /// <summary>
        /// Authenticates the credentials and returns the Identity if valid
        /// </summary>
        /// <param name="userName">The handle used to login</param>
        /// <param name="password">The password used to login</param>
        /// <returns>The identity or null if not authenticated</returns>
        public Identity GetIdentity(string userName, string password)
        {
            try
            {
                // Query the database for identity with provided username
                var identity = _data.Identities.FirstOrDefault(i => i.UserName.Equals(userName));

                // If identity is found (not null) and provided password matches stored password, return identity. Otherwise return null
                return identity != null && _crypto.Decrypt(identity.PasswordHash, password, userName, "PASSWORD") ? identity : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Authenticates the credentials and updates the Identity if valid
        /// </summary>
        /// <param name="userName">The handle used to login</param>
        /// <param name="password">The password used to login</param>
        /// <param name="firstName">The first name of the identity</param>
        /// <param name="lastName">The last name of the identity</param>
        /// <param name="address">The address of the identity</param>
        /// <param name="postcode">The postcode of the identity</param>
        /// <param name="mobile">The mobile phone number of the identity</param>
        /// <param name="email">The email address of the identity</param>
        /// <returns>The updated Identity or null if not authenticated</returns>
        public Identity UpdateIdentity(
            string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email)
        {
            try
            {
                // Query the database for identity with provided username
                var identity = _data.Identities.FirstOrDefault(i => i.UserName.Equals(userName));

                // If identity not found or provided password incorrect return null
                if (identity == null || !_crypto.Decrypt(identity.PasswordHash,  password,identity.UserName, "PASSWORD")) return null;

                // Otherwise, update with provided details, save to database and return updated identity
                identity.Update(firstName, lastName, address, postcode, mobile, email);
                _data.Identities.Update(identity);
                _data.SaveChanges();
                return identity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Deletes the provided identity from the database
        /// </summary>
        /// <param name="identity">The identity to be deleted</param>
        /// <returns>True if delete was successful, false if error was caught</returns>
        public bool DeleteIdentity(Identity identity)
        {
            try
            {
                // Delete the provided identity and write changes to database
                _data.Identities.Remove(identity);
                _data.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}