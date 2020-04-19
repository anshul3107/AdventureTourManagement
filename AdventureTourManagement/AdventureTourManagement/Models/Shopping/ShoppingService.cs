using AdventureTourManagement.Interface;
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
        private Authentication authentication;
        private IConnect _connectService;
        public ShoppingService(ATMDbContext dbContext, IServiceProvider provider, IConnect connectService)
        {
            var secureaccessFactory = new SecureAccessFactory();
            authentication = secureaccessFactory.CreateInstance(provider).SecureAccess.GetSecureAccess;
            _dbContext = dbContext;
            _connectService = connectService;
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

            if (cartItem != null)
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

        public async Task<Guid> AuthenticateUser(string userEmail)
        {
            AuthenticationInput authInputs = new AuthenticationInput();
            authInputs.AuthenticationType = Constants.AuthenticationType.Email;
            authInputs.AuthenticationMode = Constants.AuthneticationMode.TokenBasedAuthention;
            authInputs.Receiver = userEmail;
            authInputs.Subject = "Adventure Tour Management Token Verification";
            authInputs.EncryptedNetworkKeyPath = await _connectService.GetConnectionAsync();
            var result = await authentication.Authenticate(authInputs);
            return result;

        }

        public async Task<List<VmBooking>> FetchAllOrders(string userEmail)
        {
            var bookings = await _dbContext.Bookings.Where(x => x.user_name == userEmail).ToListAsync();
            var result = _dbContext.Activities.Join(bookings, a => a.activity_id, b => b.activity_id, (a, b) => new VmBooking
            {
                ActivityId = a.activity_id,
                ActivityName = a.activity_name,
                ActivityFee = a.activity_fee,
                ActivityDesc = a.activity_description,
                BookingDate = b.booking_date,
                ActivityImage = a.activity_image_path,
                UserEmail = b.user_name
            }).ToList();
            return result;
        }

        public async Task<VMActivityCart> FetchShoppingCart(string userEmail = null)
        {
            var result = await _dbContext.ActivityCart.AsNoTracking().Where(x => x.Username == userEmail)
                .Select(x => new { cartItems = JsonConvert.DeserializeObject<List<ActivityCartDTO>>(x.ItemDetails), cartId = x.Id }).FirstOrDefaultAsync();
            VMActivityCart cart = new VMActivityCart
            {
                CartId = result.cartId,
                CartItem = result.cartItems
            };
            return cart;
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
        public async Task SendBookingConfirmation(string userEmail, int cartId)
        {
            var cartDetails = await _dbContext.ActivityCart.AsNoTracking().Where(x => x.Id == cartId).FirstOrDefaultAsync();
            List<ActivityCartDTO> activities = JsonConvert.DeserializeObject<List<ActivityCartDTO>>(cartDetails.ItemDetails);
            List<Bookings> bookings = new List<Bookings>();
            List<Activities> activityEntites = _dbContext.Activities.AsNoTracking().Join(activities, x => x.activity_id, y => y.ActivityID, (x, y) => x).ToList();

            foreach (var item in activities)
            {
                var tempBooking = new Bookings
                {
                    activity_id = item.ActivityID,
                    booking_date = DateTime.Now,
                    user_name = userEmail
                };
                bookings.Add(tempBooking);

            }

            activityEntites.ForEach(x => x.activity_slots = (x.activity_slots - 1));
            _dbContext.Activities.UpdateRange(activityEntites);
            _dbContext.ActivityCart.Remove(cartDetails);
            await _dbContext.Bookings.AddRangeAsync(bookings);
            await _dbContext.SaveChangesAsync();


            SendCommunications comms = new SendCommunications();
            string mailText = "Hi, Your booking has been confirmed. If you are not a registered user, use the same email id to register.";
            EmailDTO dto = new EmailDTO()
            {
                MailMessage = mailText,
                MailSubject = "Booking Confirmation Email",
                MailTo = userEmail,
                EncryptedNetworkKeyPath = await _connectService.GetConnectionAsync()
            };
            await comms.SendEmail(dto);
        }

        public async Task<bool> VerifyUserToken(string userEmail, Guid transactionID, string token)
        {
            VerificationInput verifInputs = new VerificationInput();
            verifInputs.TransactionIdentifier = transactionID;
            verifInputs.TransactionToken = token;

            bool verificationresult = await authentication.Verify(verifInputs);

            return verificationresult;
        }
    }

}
