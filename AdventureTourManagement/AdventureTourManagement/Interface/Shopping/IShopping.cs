using AdventureTourManagement.Models;
using AdventureTourManagement.Models.Shopping;
using AdventureTourManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface.Shopping
{
   public interface IShopping
    {
        Task<Guid> AuthenticateUser(string userEmail);
        Task<bool> VerifyUserToken(string userEmail, Guid transactionID, string token);
        Task SendBookingConfirmation(string userEmail);
        Task<List<Bookings>> FetchAllOrders(string userEmail);
        Task<List<ActivityCartDTO>> FetchShoppingCart(string userEmail = null);
        Task<ActivityCart> AddToCart(int activityId, string userEmail = null);
        Task<ActivityCart> RemoveFromCart(int activityId, string userEmail = null);
    }
}
