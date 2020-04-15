using SecureAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SecureAccess.Interface
{
   public interface IAuthentication
    {
        Task<Guid> Authenticate(AuthenticationInput authInputs);
        Task<bool> Verify(VerificationInput userInputs );
    }
}
