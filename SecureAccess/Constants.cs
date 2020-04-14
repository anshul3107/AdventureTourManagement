using System;
using System.Collections.Generic;
using System.Text;

namespace SecureAccess
{
   public static class Constants
    {
        public const string SA_CONNECT = "\\SAConnect\\SAConnect.txt";
        public const string PROVIDER = "www.gmail.com";
        public const int TMNRANGE = 10000000;
        public const int TMXRANGE = 99999999;

        public  enum AuthneticationMode
        {
            WebBasedAuthentication,
            TokenBasedAuthention
        }

       public enum AuthenticationType
        {
            Email,
            Text
        }
    }
}
