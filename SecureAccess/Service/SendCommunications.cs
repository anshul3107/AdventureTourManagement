using Newtonsoft.Json;
using SecureAccess.Helper;
using SecureAccess.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecureAccess.Service
{
    public class SendCommunications
    {
        public async Task SendEmail(EmailDTO emailDTO)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(emailDTO.MailTo);
                message.Subject = emailDTO.MailSubject;
                message.Body = emailDTO.MailMessage;
                message.From = new MailAddress("adventuretourmanagement@gmail.com");
                SmtpClient client = await EmailConnection(emailDTO.EncryptedNetworkKeyPath);
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<SmtpClient> EmailConnection(string encryptedKey)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                EncryptionDecryption encryption = EncryptionDecryption.CreateInstance();
                
                if (!string.IsNullOrEmpty(encryptedKey))
                {
                    NetworkKeyDTO creds = JsonConvert.DeserializeObject<NetworkKeyDTO>(encryptedKey);
                    client.Credentials = new NetworkCredential(encryption.DecryptText(creds.EncryptedUserName, creds.EncryptedUserMessage),
                        encryption.DecryptText(creds.EncryptedKey,creds.EncryptedMessage));
                }

                return client;
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
                throw ep;
            }
        }
    }
   
}
