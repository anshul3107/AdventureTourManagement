using System;

namespace AdventureTourManagement.ViewModels
{
    public class VMUserDetail : VMActivityDetails
    {
        public string user_email { get; set; }
        public Guid userAuthID { get; set; }
        public string Token { get; set; }
        public bool IsToken { get; set; }
        public string Message { get; set; }
    }
}