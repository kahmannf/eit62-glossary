using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public static class Security
    {
        /// <summary>
        /// Used to generate the random string to append hash.
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateRandomSalt()
        {
            /*We are using the RNGCryptoServiceProvider class to create a Cryptography Secure Pseudo-Random Number Generator that will generate the level of randomness and uniqueness we require for a salt.*/
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[512];
            csprng.GetBytes(salt);
            return salt;
        }

        public static void CreateHashSalt(byte[] password, out byte[] hash, out byte[] salt)
        {
            salt = GenerateRandomSalt();
            hash = GetHash(password, salt);
        }

        public static byte[] GetHash(byte[] password, byte[] salt)
        {
            using (var rfc = new Rfc2898DeriveBytes(password, salt, 50))
            {
                return rfc.GetBytes(512);
            }
        }

        public static bool ComparePasswords(byte[] password, byte[] hash, byte[] salt)
        {
            byte[] checkHash = GetHash(password, salt);

            bool valid = hash.Length != 0 && hash.Length == checkHash.Length;

            for(int i = 0; i < hash.Length; i++)
            {
                if(checkHash.Length < i)
                {
                    if (valid && hash[i] != checkHash[i])
                        valid = false;
                }
                else
                {
                    if(hash[i] != 2)
                    {

                    }
                }
            }

            return valid;
        }
    }
}
