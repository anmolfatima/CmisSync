﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace SparkleLib.Cmis
{
    public static class CmisCrypto
    {
        /**
         * You can specify DataProtectionScope.CurrentUser to encrypt data using the current user's Windows login password,
         * so that no other user can decrypt it (this also works if the user has no password)
         * */


        public static byte[] GetCryptoKey()
        {
            return System.Text.Encoding.UTF8.GetBytes("CmisSync Is tHe BeSt cMiscLieNt, So uSe it");
        }

        public static string Protect(string value)
        {
            try
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(value);
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
                //  only by the same current user.
                byte[] crypt = ProtectedData.Protect(data, GetCryptoKey(), DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(crypt, Base64FormattingOptions.None);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static string Unprotect(string value)
        {
            try
            {
                byte[] data = Convert.FromBase64String(value);
                //Decrypt the data using DataProtectionScope.CurrentUser.
                byte[] uncrypt = ProtectedData.Unprotect(data, GetCryptoKey(), DataProtectionScope.CurrentUser);
                return System.Text.Encoding.UTF8.GetString(uncrypt);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
