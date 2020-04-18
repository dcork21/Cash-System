using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace CashSystemMVC.Interfaces.System
{
    /// <summary>
    /// The cryptography library, used for secure authentication throughout the system
    /// </summary>
    public interface ICrypto
    {
        string Encrypt(string password, string secret, string associatedData);
        bool Decrypt(string hashString, string password, string secret, string associatedData);
    }

    /// <summary>
    /// Implementation of a crypto library following Nuget Package Source Guide: https://github.com/mheyman/Isopoh.Cryptography.Argon2#usage
    /// </summary>
    public class Crypto : ICrypto
    {
        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        /// <summary>
        /// Produces an encryption hash using the Argon2 encryption library
        /// </summary>
        /// <param name="password">The plain text string to be encrypted</param>
        /// <param name="secret">A secret string to provide obfuscation</param>
        /// <param name="associatedData">A string indicating what the hash was produced for</param>
        /// <returns></returns>
        public string Encrypt(string password, string secret, string associatedData)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            byte[] associatedDataBytes = Encoding.UTF8.GetBytes(associatedData);
            byte[] salt = new byte[16];
            Rng.GetBytes(salt);
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = Environment.ProcessorCount,
                Password = passwordBytes,
                Salt = salt, 
                Secret = secretBytes,
                AssociatedData = associatedDataBytes,
                HashLength = 20
            };
            var argon2A = new Argon2(config);
            string hashString;
            using (SecureArray<byte> hashA = argon2A.Hash())
            {
                hashString = config.EncodeString(hashA.Buffer);
            }
            return hashString;
        }

        /// <summary>
        /// Decrypts and verifies a hashString, 
        /// </summary>
        /// <param name="hashString">The encrypted password to verify</param>
        /// <param name="password">The plain text string to verify against the hash string</param>
        /// <param name="secret">The obfuscation string</param>
        /// <param name="associatedData">A string indicating what the hash was produced for</param>
        /// <returns></returns>
        public bool Decrypt(string hashString, string password, string secret, string associatedData)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            byte[] associatedDataBytes = Encoding.UTF8.GetBytes(associatedData);
            byte[] salt = new byte[16];
            var configOfPasswordToVerify = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = Environment.ProcessorCount,
                Password = passwordBytes,
                Salt = salt,
                Secret = secretBytes,
                AssociatedData = associatedDataBytes,
                HashLength = 20
            };
            SecureArray<byte> hashB = null;
            try
            {
                if (!configOfPasswordToVerify.DecodeString(hashString, out hashB) || hashB == null) return false;
                var argon2ToVerify = new Argon2(configOfPasswordToVerify);
                using var hashToVerify = argon2ToVerify.Hash();
                return !hashB.Buffer.Where((b, i) => b != hashToVerify[i]).Any();
            }
            finally
            {
                hashB?.Dispose();
            }
        }
    }
}
