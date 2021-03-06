﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureAccess.Helper
{
    public class EncryptionDecryption
    {
        private static readonly EncryptionDecryption _encryption = new EncryptionDecryption();

        public static EncryptionDecryption CreateInstance() => _encryption;

        private EncryptionDecryption()
        {
        }

        public void FileEncryption(string FileName)
        {
            File.Encrypt(FileName);
        }

        // Decrypt file.
        public void FileDecryption(string FileName)
        {
            File.Decrypt(FileName);
        }

        public bool CompareStrings(string input, string sourceText, string passKey)
        {
            var diText = DecryptText(sourceText, passKey);
            var dinput = DecryptText(input, passKey);

            if (diText.Equals(dinput))
                return true;
            else
                return false;
        }

        //Encrypt - Decrypt Password

        public string EncryptText(string textToEncrypt, string passKey)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(textToEncrypt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passKey);

            //Hashing the password with SHA256

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public string DecryptText(string textToDecrypt, string passKey)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(textToDecrypt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passKey);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;

        }
        private byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passKeyBytes)
        {
            byte[] encryptedBytes = null;

            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passKeyBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        ExecuteMethod(() => cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length));
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        private byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passKeyBytes)
        {
            byte[] decryptedBytes = null;

            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passKeyBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        ExecuteMethod(() => cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length));
                        cs.Close();

                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }


        private void ExecuteMethod(Action functoRun)
        {
            functoRun();
        }
    }
}
