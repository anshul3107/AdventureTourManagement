using AdventureTourManagement.Interface.Shopping;
using AdventureTourManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SecureAccess;
using SecureAccess.Model;
using SecureAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Models.Shopping
{
    public class ShoppingService : IShopping
    {
        private readonly ATMDbContext _dbContext;

        public ShoppingService(ATMDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ActivityCart> AddToCart(int activityId, string userEmail = null)
        {
            var cartItem = _dbContext.ActivityCart.Any(x => x.Username == userEmail) ? 
                _dbContext.ActivityCart.AsNoTracking().Where(x => x.Username == userEmail).FirstOrDefault() : null;
            List<ActivityCartDTO> dtoLst = new List<ActivityCartDTO>();

            if (cartItem != null)
            {
                if (!string.IsNullOrEmpty(cartItem.ItemDetails))
                {
                    dtoLst = JsonConvert.DeserializeObject<List<ActivityCartDTO>>(cartItem.ItemDetails);
                }
            }
            
            var activityDetails = _dbContext.Activities.Where(x => x.activity_id == activityId).FirstOrDefault();
            ActivityCartDTO dto = new ActivityCartDTO
            {
                ActivityID = activityDetails.activity_id,
                ActivityDescription = activityDetails.activity_description,
                ActivityFee = activityDetails.activity_fee,
                ActivityName = activityDetails.activity_name,
                ActivityImage = activityDetails.activity_image_path,
                Quantity = 1
            };

            dtoLst.Add(dto);
           

            string itemJson = JsonConvert.SerializeObject(dtoLst);
            if (string.IsNullOrEmpty(userEmail))
            {
                return null;
               
            }
            
            ActivityCart entity = new ActivityCart
            {
                ItemDetails = itemJson,
                Username = userEmail
            };

            ActivityCart result = new ActivityCart();

            if(cartItem != null)
            {
                 entity.Id = cartItem.Id;
                 var exeResult = _dbContext.ActivityCart.Update(entity);
                 result = exeResult.Entity;
            }
            else
            {
                var exeResult = await _dbContext.ActivityCart.AddAsync(entity);
                result = exeResult.Entity;
            }
         
            await _dbContext.SaveChangesAsync();
            return result;
          
        }

        private ActivityCartDTO FetchCartDTO(int activityID)
        {
            var activityDetails = _dbContext.Activities.Where(x => x.activity_id == activityID).FirstOrDefault();
            ActivityCartDTO dto = new ActivityCartDTO
            {
                ActivityID = activityDetails.activity_id,
                ActivityDescription = activityDetails.activity_description,
                ActivityFee = activityDetails.activity_fee,
                ActivityName = activityDetails.activity_name,
                Quantity = 1
            };

            return dto;
        }

        public Task<Guid> AuthenticateUser(string userEmail)
        {
            Authentication authentication = new Authentication();

            AuthenticationInput authInputs = new AuthenticationInput();
            authInputs.AuthenticationType = Constants.AuthenticationType.Email;
            authInputs.AuthenticationMode = Constants.AuthneticationMode.TokenBasedAuthention;
            authInputs.Receiver = userEmail;
            authInputs.Subject = "Adventure Tour Management Token Verification";

            var result = authentication.Authenticate(authInputs);
            return result;
        }

        public async Task<List<Bookings>> FetchAllOrders(string userEmail)
        {
            var userId = _dbContext.UserDetails.Where(x => x.emial == userEmail).FirstOrDefault().Reg_id;
            var result = await _dbContext.Bookings.Where(x => x.user_id.Value == userId).ToListAsync();
            return result;
        }

        public async Task<List<ActivityCartDTO>> FetchShoppingCart(string userEmail = null)
        {
            var result = await _dbContext.ActivityCart.AsNoTracking().Where(x => x.Username == userEmail)
                .Select(x => JsonConvert.DeserializeObject<List<ActivityCartDTO>>(x.ItemDetails)).FirstOrDefaultAsync();
            
            return result;
        }

        public async Task<ActivityCart> RemoveFromCart(int activityId, string userEmail = null)
        {
            var entity = await _dbContext.ActivityCart.AsNoTracking().Where(x => x.Username == userEmail).FirstOrDefaultAsync();
            var cartItems = JsonConvert.DeserializeObject<List<ActivityCartDTO>>(entity.ItemDetails);
            
            var actDTO = FetchCartDTO(activityId);
            cartItems.RemoveAll(x => x.ActivityID == actDTO.ActivityID);
            var itemJson = JsonConvert.SerializeObject(cartItems);
            entity.ItemDetails = itemJson;

            var result = _dbContext.ActivityCart.Update(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        //TODO : update email text
        public async Task SendBookingConfirmation(string userEmail)
        {
            SendCommunications comms = new SendCommunications();
            string mailText = "Hi, Your booking has been confirmed. If you are not a registered user, use the same email id to register.";
            EmailDTO dto = new EmailDTO()
            {
                MailMessage = mailText,
                MailSubject = "Booking Confirmation Email",
                MailTo = userEmail
                
            };
            await comms.SendEmail(dto);
        }

        public async Task<bool> VerifyUserToken(string userEmail, Guid transactionID, string token)
        {
            VerificationInput verifInputs = new VerificationInput();
            Authentication authentication = new Authentication();
            verifInputs.TransactionIdentifier = transactionID;
            verifInputs.TransactionToken = token;

            bool verificationresult = await authentication.Verify(verifInputs);

            return verificationresult;
        }
    }
}
