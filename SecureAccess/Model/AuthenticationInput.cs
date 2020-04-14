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
    }

    public class VerificationInput
    {
        public Guid TransactionIdentifier { get; set; }
        public string TransactionToken { get; set; }
    }
}
