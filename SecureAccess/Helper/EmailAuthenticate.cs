using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SecureAccess.Model;
using SecureAccess.Service;

namespace SecureAccess.Helper
{
    public  class EmailAuthenticate
    {

        public async Task<Guid> SendAuthenticationEmail(AuthenticationInput input)
        {
            EmailDTO messageDTO = new EmailDTO();
            var transactId = Guid.NewGuid();

            if (input.AuthenticationMode == Constants.AuthneticationMode.WebBasedAuthentication)
                messageDTO.MailMessage = await MailTextAsync(input.AuthenticationMode, transactId, input.VerificationLink);
            else
                messageDTO.MailMessage = await MailTextAsync(input.AuthenticationMode, transactId);

            messageDTO.MailSubject = input.Subject;
            messageDTO.MailTo = input.Receiver;
            messageDTO.EncryptedNetworkKeyPath = input.EncryptedNetworkKeyPath;
            SendCommunications comms = new SendCommunications();
            await comms.SendEmail(messageDTO);

            return transactId;

        }


        private async Task<string> MailTextAsync(Constants.AuthneticationMode mode,Guid transactID, string url = null)
        {
            string result = string.Empty;
            
            if(mode == Constants.AuthneticationMode.TokenBasedAuthention)
            {
                Random random = new Random();
                var token = random.Next(Constants.TMNRANGE, Constants.TMXRANGE);

                result = string.Format("Please use below token to confirm email verification. {0}", token);
               await CreateEncryptedFileAsync(token.ToString(),transactID);
            }
            else
            {
                result = string.Format("Please use below link to confirm email verification. {0}", url);
            }

            return result;

        }

        private async Task CreateEncryptedFileAsync(string token, Guid transactionId)
        {
            EncryptionDecryption coded = EncryptionDecryption.CreateInstance();

            string eToken = coded.EncryptText(token,"encryptionKey");

            //string filepath = "\\TransactFiles\\" + transactionId + ".txt";

            string filepath = "/local/temp/" + transactionId + ".txt";

            if (!Directory.Exists("/local/temp/"))
            {
                Directory.CreateDirectory("/local/temp/");
            }

            //if (! Directory.Exists("\\TransactFiles"))
            //{
            //    Directory.CreateDirectory("\\TransactFiles");
            //}

            if (! File.Exists(filepath))
            {
               await File.WriteAllTextAsync(filepath, eToken);
            }

           // coded.FileEncryption(filepath);

        }
    }
}


