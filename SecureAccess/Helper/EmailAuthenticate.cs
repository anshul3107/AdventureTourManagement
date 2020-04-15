using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace SecureAccess.Helper
{
    public class EmailAuthenticate
    {
        public async System.Threading.Tasks.Task<Guid> SendEmailAsync(Model.AuthenticationInput input)
        {
            Guid transactId = Guid.Empty;

            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(input.Receiver);
                message.Subject = input.Subject;

                transactId = Guid.NewGuid();

                if (input.AuthenticationMode == Constants.AuthneticationMode.WebBasedAuthentication)
                    message.Body = MailText(input.AuthenticationMode, transactId, input.VerificationLink);
                else
                    message.Body = MailText(input.AuthenticationMode, transactId);

                SmtpClient client = new SmtpClient(Constants.PROVIDER);
                var credentials = (await File.ReadAllLinesAsync(Constants.SA_CONNECT)).ToList();

                client.Credentials = new NetworkCredential(credentials[0], credentials[1]);

                await client.SendMailAsync(message);

            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }

            return transactId;
        }

        private string MailText(Constants.AuthneticationMode mode,Guid transactID, string url = null)
        {
            string result = string.Empty;
            
            if(mode == Constants.AuthneticationMode.TokenBasedAuthention)
            {
                Random random = new Random();
                var token = random.Next(Constants.TMNRANGE, Constants.TMXRANGE);

                result = string.Format("Please use below token to confirm email verification. {0}", token);
                CreateEncryptedFile(token.ToString());
            }
            else
            {
                result = string.Format("Please use below link to confirm email verification. {0}", url);
            }

            return result;

        }

        private Guid CreateEncryptedFile(string token)
        {
            Guid transactionId = Guid.NewGuid();

            EncryptionDecryption coded = new EncryptionDecryption();

            string eToken = coded.EncryptText(token,"encryptionKey");

            string filepath = "\\TransactFiles\\" + transactionId + ".txt";

            if (! Directory.Exists("\\TransactFiles"))
            {
                Directory.CreateDirectory("\\TransactFiles");
            }
            
            if (! File.Exists(filepath))
            {
                File.WriteAllTextAsync(filepath, eToken);
            }

            coded.FileEncryption(filepath);

            return transactionId;
        }
    }
}


