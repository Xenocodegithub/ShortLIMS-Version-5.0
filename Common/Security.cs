using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
namespace LIMS_DEMO.Common
{
    public sealed class Security
    {
        private const int iSaltSize = 32;
        private const int iHashSize = 40;
        private static string Hash(string password, int iterations)
        {
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[iSaltSize]);
            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(iHashSize);
            //combine salt and hash
            var hashBytes = new byte[iSaltSize + iHashSize];
            Array.Copy(salt, 0, hashBytes, 0, iSaltSize);
            Array.Copy(hash, 0, hashBytes, iSaltSize, iHashSize);
            //convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);
            //format hash with extra information
            return string.Format("$SECURE${0}${1}", iterations, base64Hash);
        }
        public static string Encrypt(string password)
        {
            return Hash(password, 2000);
        }
        private static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$SECURE$");
        }
        public static bool Verify(string password, string hashedPassword)
        {
            //check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The password is not encrypted.");
            }
            //extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$SECURE$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];
            //get hashbytes
            var hashBytes = Convert.FromBase64String(base64Hash);
            //get salt
            var salt = new byte[iSaltSize];
            Array.Copy(hashBytes, 0, salt, 0, iSaltSize);
            //create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(iHashSize);
            //get result
            for (var i = 0; i < iHashSize; i++)
            {
                if (hashBytes[i + iSaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}