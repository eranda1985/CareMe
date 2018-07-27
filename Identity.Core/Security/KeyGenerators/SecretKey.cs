using System;
using System.Security.Cryptography;
using Identity.Core.Exceptions;

namespace Identity.Core.Security.KeyGenerators
{
    public class SecretKey
    {
        private  static RNGCryptoServiceProvider _randomCryptoProvider = new RNGCryptoServiceProvider();

        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <returns>The key.</returns>
        /// <param name="keyLength">Key length.</param>
        public static string GenerateKey(int keyLength)
        {
            if(keyLength < 32)
            {
                throw new ValidationException();
            }

            var keyBytes = CreateKey(keyLength);

            if(!ValidateKey(keyLength, keyBytes))
            {
                throw new ValidationException();
            }

            return ConvertToString(keyBytes);
        }


        /// <summary>
        /// Creates the key as bytes.
        /// </summary>
        /// <returns>The key.</returns>
        /// <param name="bytesLength">Bytes length.</param>
        private static byte[] CreateKey(int bytesLength)
        {
            if(bytesLength < 32 )
            {
                throw new ValidationException();
            }

            byte[] bytes = new byte[bytesLength];

            _randomCryptoProvider.GetNonZeroBytes(bytes);

            SHA256CryptoServiceProvider sHA256Crypto = new SHA256CryptoServiceProvider();

            return sHA256Crypto.ComputeHash(bytes); // <- This allows us to keep the key in alphanumeric format. 
        }

        /// <summary>
        /// Validates the key.
        /// </summary>
        /// <returns><c>true</c>, if key was validated, <c>false</c> otherwise.</returns>
        /// <param name="keyLength">Key length.</param>
        /// <param name="key">Key.</param>
        private static bool ValidateKey(int keyLength, byte[] key)
        {
            return key.Length == keyLength;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>The to string.</returns>
        /// <param name="keyBytes">Key bytes.</param>
        private static string ConvertToString(byte[] keyBytes)
        {
            string keyStr = "";

            for (int i = 0; i < keyBytes.Length;i++)
            {
                keyStr += keyBytes[i].ToString("x2"); // <- lowecase 
            }

            return keyStr;
        }
    }
}
