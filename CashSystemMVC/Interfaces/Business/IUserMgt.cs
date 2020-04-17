using System;
using System.Linq;
using CashSystemMVC.Models;

namespace CashSystemMVC.Interfaces.Business
{
    /// <summary>
    ///     The Business Interface which manages the User records
    /// </summary>
    public interface IUserMgt
    {
        User CreateUser(string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email);

        User GetUser(string userName, string password);

        User UpdateUser(string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email);

        bool DeleteUser(string userName, string password);
    }

    /// <summary>
    ///     The realization of the User manager interface
    /// </summary>
    public class UserMgt : IUserMgt
    {
        // The database
        private readonly DataContext _data;
        private readonly IdentityMgt _identityMgt;

        /// <summary>
        ///     The constructor for the realization of the User Manager
        /// </summary>
        /// <param name="data">The database context dependency injected via StartUp</param>
        /// <param name="identityMgt">The identity manager interface injected via Startup</param>
        public UserMgt(DataContext data, IdentityMgt identityMgt)
        {
            _data = data;
            _identityMgt = identityMgt;
        }

        /// <summary>
        ///     Creates a new User record
        /// </summary>
        /// <param name="userName">The handle used to login</param>
        /// <param name="password">The password used to login</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="address">The address of the user</param>
        /// <param name="postcode">The postcode of the user</param>
        /// <param name="mobile">The mobile phone number of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <returns>The created User</returns>
        public User CreateUser(string userName,
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
                // Validate all provided attributes, return null if absent
                if (string.IsNullOrEmpty(userName) ||
                    string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(firstName) ||
                    string.IsNullOrEmpty(lastName) ||
                    string.IsNullOrEmpty(address) ||
                    string.IsNullOrEmpty(postcode) ||
                    string.IsNullOrEmpty(mobile) ||
                    string.IsNullOrEmpty(email)
                ) return null;

                // Create the identity first
                var identity = _identityMgt.CreateIdentity(userName, password, firstName, lastName, address,
                    postcode, mobile, email);

                // If null (could not be created) return null
                if (identity == null) return null;

                // Otherwise create the new user with the created identity
                var user = new User {IdentityId = identity.Id};

                // Save to database and return
                _data.Users.Add(user);
                _data.SaveChanges();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Authenticates the credentials and returns the User if valid
        /// </summary>
        /// <param name="userName">The handle used to login</param>
        /// <param name="password">The password used to login</param>
        /// <returns>The User or null if not authenticated</returns>
        public User GetUser(string userName, string password)
        {
            try
            {
                // Get identity, will return null if not authenticated
                var identity = _identityMgt.GetIdentity(userName, password);

                // If not authenticated (return null), otherwise return the user record linked to the provided identity
                return identity == null ? null : _data.Users.FirstOrDefault(u => u.IdentityId == identity.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        ///     Authenticates the credentials and updates the User if valid
        /// </summary>
        /// <param name="userName">The handle used to login</param>
        /// <param name="password">The password used to login</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <param name="address">The address of the user</param>
        /// <param name="postcode">The postcode of the user</param>
        /// <param name="mobile">The mobile phone number of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <returns>The updated User or null if not authenticated</returns>
        public User UpdateUser(string userName,
            string password,
            string firstName,
            string lastName,
            string address,
            string postcode,
            string mobile,
            string email)
        {
            // Get identity, will return null if not authenticated
            var identity = _identityMgt.GetIdentity(userName, password);

            // Not authenticated so return null
            if (identity == null) return null;

            // Otherwise update identity record with provided data
            _identityMgt.UpdateIdentity(userName, password, firstName, lastName, address, postcode, mobile, email);

            // And return the linked user
            return _data.Users.FirstOrDefault(u => u.IdentityId == identity.Id);
        }

        /// <summary>
        ///     Deletes the User and Identity if authenticated
        /// </summary>
        /// <param name="userName">The handle used to authenticate</param>
        /// <param name="password">The password used to authenticate</param>
        /// <returns>True if successfully deleted, false if not</returns>
        public bool DeleteUser(string userName, string password)
        {
            try
            {
                // Get identity, will return null if not authenticated

                var identity = _identityMgt.GetIdentity(userName, password);

                // Not authenticated so return false

                if (identity == null) return false;

                // Otherwise get User with linked identity
                var user = _data.Users.FirstOrDefault(u => u.IdentityId == identity.Id);

                // if no linked user return false - cannot delete (identity has been orphaned)
                if (user == null)
                {
                    Console.WriteLine("ERROR: No user linked to Identity");
                    return false;
                }

                // Attempt to delete Identity (if returns false, delete failed so return false)
                if (!_identityMgt.DeleteIdentity(identity)) return false;

                // Otherwise can finish up by deleting user, writing to database and returning true (success)
                _data.Users.Remove(user);
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