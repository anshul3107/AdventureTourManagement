using AdventureTourManagement.Models;
using AdventureTourManagement.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface.User
{
   public interface IUser
    {
        Task<VmUser> GetUserProfile(string user_email);
        Task<VmUser> UpdateUserDetails(VmUser userDets);
        Task<VmUser> UpdatePassword(VmUser userDets);
        Task<VmUser> ValidateUserLogin(VmUser userLogin);
        Task<VmUser> AddNewUser(VmUser userInput);
        Task ProvideFeedback(int activityId, int activityRating);
    }
}
