using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public static class Encryption
    {
        public const string initVector = "tusada89geji340t89u2";
        public const int keysize = 256;

        public static string Decrypt(string encryptedText)
        {
            try
            {
                string passPhrase = "staffKPIEncryptionKey";
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string DecryptStaffKPI(string encryptedText)
        {
            try
            {
                byte[] decryptedBytes = Convert.FromBase64String(encryptedText);
                return Encoding.Unicode.GetString(ChangeBytes(decryptedBytes));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Encrypt(string plainText)
        {
            string passPhrase = "staffKPIEncryptionKey";
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);////To encrypt
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string EncryptStaffKPI(string encryptedText)
        {
            try
            {
                byte[] encryptedBytes = Encoding.Unicode.GetBytes(encryptedText);
                encryptedBytes = ChangeBytes(encryptedBytes);
                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static byte[] ChangeBytes(byte[] bytes)
        {
            byte tmpByte;
            tmpByte = bytes[4];
            bytes[4] = bytes[1];
            bytes[1] = tmpByte;
            tmpByte = bytes[6];
            bytes[6] = bytes[0];
            bytes[0] = tmpByte;
            return bytes;
        }
    }


}
