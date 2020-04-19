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
        Task SendBookingConfirmation(string userEmail, int cartId);
        Task<List<VmBooking>> FetchAllOrders(string userEmail);
        Task<VMActivityCart> FetchShoppingCart(string userEmail = null);
        Task<ActivityCart> AddToCart(int activityId, string userEmail = null);
        Task<ActivityCart> RemoveFromCart(int activityId, string userEmail = null);
    }
}
