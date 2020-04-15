using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecureAccess.Helper
{
    public class EncryptionDecryption
    {
        //Encrypt - Decrypt File
        //public void EncryptDecryptFile(string filename)
        //{
        //    try
        //    {
        //        Console.WriteLine("Encrypt " + filename);

        //        // File Encryption
        //        FileEncryption(filename);

        //        Console.WriteLine("Decrypt " + filename);

        //        // File Decryption
        //        FileDecryption(filename);

        //        Console.WriteLine("Done");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }

        //    Console.ReadLine();
        //}

        // Encrypt file.
        public void FileEncryption(string FileName)
        {
            File.Encrypt(FileName);
        }

        // Decrypt file.
        public void FileDecryption(string FileName)
        {
            File.Decrypt(FileName);
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
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passKeyBytes)
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

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passKeyBytes)
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


        public void ExecuteMethod(Action functoRun)
        {
            functoRun();
        }
    }
}
