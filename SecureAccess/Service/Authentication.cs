
using Microsoft.AspNetCore.Mvc.Filters;
using SecureAccess.Helper;
using SecureAccess.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SecureAccess
{
    public class Authentication
    {
        private static readonly Authentication _Authentication = new Authentication();

        public static Authentication CreateInstance() => _Authentication;

        private Authentication()
        {

        }
        public async Task<Guid> Authenticate(AuthenticationInput authInputs)
        {
            
            EmailAuthenticate authenticationByEmail = new EmailAuthenticate();
       
            var transactID =  await authenticationByEmail.SendAuthenticationEmail(authInputs);

            return transactID; 
            
        }

        public async Task<bool> Verify(VerificationInput verifInputs)
        {
            TwoStepAuth auth = new TwoStepAuth();
            EncryptionDecryption verifyToken = auth.GetEncryptionDecryption;

            string filepath = "\\TransactFiles\\" + verifInputs.TransactionIdentifier + ".txt";

           // verifyToken.FileDecryption(filepath);

            DateTime fileCreationDate = File.GetCreationTime(filepath);

            if (DateTime.Now.Subtract(fileCreationDate).TotalSeconds < 600) 
            {

                if (File.Exists(filepath))
                {
                    var fileText = await File.ReadAllTextAsync(filepath);
                    if (!string.IsNullOrEmpty(fileText))
                    {
                        string decrypted = verifyToken.DecryptText(fileText.Trim(), "encryptionKey");

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

    public class TwoStepAuth : IDisposable
    {
        public bool IsReusable => throw new NotImplementedException();

        public Authentication GetSecureAccess
        {
            get; set;
        }

        public EncryptionDecryption GetEncryptionDecryption
        {
            get; set;
        }

        public TwoStepAuth()
        {
            GetEncryptionDecryption = EncryptionDecryption.CreateInstance();
            GetSecureAccess = Authentication.CreateInstance();

        }
      
        public void Dispose()
        {
            GetSecureAccess = null;
        }
    }

    public class SecureAccessFactory
    {
        private TwoStepAuth _twoStepAuth;
        public TwoStepAuth SecureAccess { get { return _twoStepAuth; } set
            {
                _twoStepAuth = value;
            }
        }

        public SecureAccessFactory CreateInstance(IServiceProvider serviceProvider)
        {
            var instance = serviceProvider.GetService(typeof(TwoStepAuth));
            return new SecureAccessFactory { SecureAccess =(TwoStepAuth) instance };
        }


    }
}
