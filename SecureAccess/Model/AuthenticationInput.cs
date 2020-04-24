using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAccess.Model
{
    public class AuthenticationInput
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string VerificationLink { get; set; }
        public Constants.AuthneticationMode AuthenticationMode { get; set; }
        public Constants.AuthenticationType AuthenticationType { get; set; }
        public string EncryptedNetworkKeyPath { get; set; }
    }

    public class NetworkKeyDTO
    {
        public string EncryptedUserName { get; set; }
        public string EncryptedUserMessage { get; set; }
        public string EncryptedKey { get; set; }
        public string EncryptedMessage { get; set; }
    }

    public class VerificationInput
    {
        public Guid TransactionIdentifier { get; set; }
        public string TransactionToken { get; set; }
    }

    public class EmailDTO
    {
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailMessage { get; set; }
        public string EncryptedNetworkKeyPath { get; set; }
    }
}
