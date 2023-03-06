using System.Security.Cryptography;
using System.Text;

namespace BucklesChatBackend.Encryption
{
    public static class PasswordEncryption
    {

        /// <summary>
        /// Returns a tuple consisting of (salt, encrypted password)
        /// </summary>
        /// <param name="password">The password to encrypt</param>
        /// <returns>A tuple of the salt used, and the encrypted password</returns>
        public static Tuple<string, string> EncryptPassword(string password)
        {
            byte[] salt = new byte[128];
            new Random().NextBytes(salt);
            string stringifiedSalt = Encoding.Default.GetString(salt);
            byte[] computedHash;
            using (HMACSHA256 hMACSHA256 = new HMACSHA256())
            {
                computedHash = hMACSHA256.ComputeHash(Encoding.ASCII.GetBytes(password + salt));
            }
            string stringifiedHash = Encoding.Default.GetString(computedHash);
            string hash = stringifiedHash.Length > 128 ? stringifiedHash.Substring(0, 128) : stringifiedHash;
            Tuple<string, string> result = new Tuple<string, string>(stringifiedSalt, hash);
            return result;
        }

        /// <summary>
        ///     Fixed encryption, used to compare passwords upon sign-in
        /// </summary>
        /// <param name="password">The password to fix encrypt</param>
        /// <param name="salt">The salt used with the password</param>
        /// <returns>The fixed encrypted password</returns>
        public static string FixedEncryption(string password, string salt)
        {
            byte[] computedHash;
            using (HMACSHA256 hMACSHA256 = new())
            {
                computedHash = hMACSHA256.ComputeHash(Encoding.ASCII.GetBytes(password + salt));
            }

            string stringifiedHash = Encoding.Default.GetString(computedHash);

            return stringifiedHash.Length > 128 ? stringifiedHash.Substring(0, 128) : stringifiedHash;
        }

    }
}
