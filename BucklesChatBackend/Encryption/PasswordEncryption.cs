using System.Security.Cryptography;
using System.Text;

namespace BucklesChatBackend.Encryption
{
    public class PasswordEncryption
    {

        public Tuple<string, string> EncryptPassword(string password)
        {
            byte[] salt = new byte[128];
            new Random().NextBytes(salt);
            string stringifiedSalt = Encoding.Default.GetString(salt);
            byte[] computedHash;
            using (HMACSHA256 hMACSHA256 = new HMACSHA256())
            {
                computedHash = hMACSHA256.ComputeHash(Encoding.ASCII.GetBytes(password + salt));
            }
            var hash = Encoding.Default.GetString(computedHash).Substring(0, 128);
            var result = new Tuple<string, string>(stringifiedSalt, hash);
            return result;
        }

    }
}
