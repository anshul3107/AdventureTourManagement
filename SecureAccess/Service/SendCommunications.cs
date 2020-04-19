using SecureAccess.Helper;
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
                SmtpClient client = await EmailConnection();
                client.EnableSsl = true;
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<SmtpClient> EmailConnection()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                EncryptionDecryption encryption = EncryptionDecryption.CreateInstance();
                string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                var assemblyPath = assemblyFile.Substring(0, assemblyFile.LastIndexOf('\\')) + "\\SAConnect\\" + "SAConnect.txt";
                //var credPath_1 = Path.Combine(assemblyPath, "SAConnect");
                //var credPath = Path.Combine(credPath_1, "SAConnect.txt" );
                if (File.Exists(assemblyPath))
                {
                    var credentials = (await File.ReadAllLinesAsync(assemblyPath)).ToList();
                    var temp = encryption.DecryptText(credentials[0], "encryptionkey");
                    var temp2 = encryption.DecryptText(credentials[1], "encryptionkey");
                    client.Credentials = new NetworkCredential(encryption.DecryptText(credentials[0], "encryptionkey"), encryption.DecryptText(credentials[1], "encryptionkey"));
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
    public class EmailDTO
    {
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailMessage { get; set; }
    }
}
