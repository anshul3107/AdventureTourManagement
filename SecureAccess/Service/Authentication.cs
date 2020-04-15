using SecureAccess.Helper;
using SecureAccess.Interface;
using SecureAccess.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SecureAccess
{
    public class Authentication : IAuthentication
    {
        public async Task<Guid> Authenticate(AuthenticationInput authInputs)
        {
            EmailAuthenticate authenticationByEmail = new EmailAuthenticate();
       
            var transactID =  await authenticationByEmail.SendEmailAsync(authInputs);

            return transactID; 
            
        }

        public async Task<bool> Verify(VerificationInput verifInputs)
        {
            EncryptionDecryption verifyToken = new EncryptionDecryption();

            string filepath = "\\TransactFiles\\" + verifInputs.TransactionIdentifier + ".txt";

            verifyToken.FileDecryption(filepath);

            DateTime fileCreationDate = File.GetCreationTime(filepath);

            if (DateTime.Now.Subtract(fileCreationDate).TotalSeconds < 600) 
            {

                if (File.Exists(filepath))
                {
                    var fileText = await File.ReadAllTextAsync(filepath);
                    if (!string.IsNullOrEmpty(fileText))
                    {
                        string decrypted = verifyToken.DecryptText(fileText, "encryptionkey");

                        if (decrypted == verifInputs.TransactionToken)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;

            }
            else
            {
                return false;
            }
        }
    }
}
