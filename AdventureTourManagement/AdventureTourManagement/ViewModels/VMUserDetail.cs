using AdventureTourManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdventureTourManagement.ViewModels
{
    public class VMUserDetail : VMActivityDetails
    {
        public string user_email { get; set; }
        public Guid userAuthID { get; set; }
        public string Token { get; set; }
        public bool IsToken { get; set; }
        public string Message { get; set; }
        public int IsForgetPassword { get; set; }
        public int cartId { get; set; }
    }

    public class VmUser
    {
        [Required(ErrorMessage = "Name cannot be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email cannot be empty")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Contact details cannot be empty")]
        public string UserContact { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string UserEncyryptedKey { get; set; }
        [Required(ErrorMessage = "Secret Message cannot be empty")]
        public string UserEncryptionMessage { get; set; }
        public string Message { get; set; }
        public string DecryptedUserEmail { get; set; }

        public VmUser(User userDets)
        {
            this.UserName = userDets.user_name;
            this.UserEmail = userDets.user_email;
            this.UserContact = userDets.user_contact;

        }

        public VmUser()
        {

        }
    }
}