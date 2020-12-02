using System.Security.Cryptography;
using System.Text;

namespace XTecDigital.Helpers
{
    public static class Encryption
    {

        public static string Md5(string input) {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static bool Matches(string unencrypted, string encrypted)
        {
            return encrypted.Equals(Md5(unencrypted));
        }
        
    }
}