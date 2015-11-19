using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YesChef_DataLayer
{
    public class EncryptionHandler
    {
        public static string CreateHash(string plainText, int saltValue)
        {
            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue.ToString());
            var plainTextWithSaltBytes = new byte[plainTextBytes.Length+saltValueBytes.Length];

            for(int i=0; i<plainTextBytes.Length; i++)
            {
                plainTextWithSaltBytes[ i] = plainTextBytes[i];
            }

            for (int i = 0; i < saltValueBytes.Length; i++)
            {
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltValueBytes[i];
            }

            var algorithm = new SHA256Managed();
            var hashBytes = algorithm.ComputeHash(plainTextWithSaltBytes);
            return Encoding.ASCII.GetString(hashBytes);
        }
    }
}
