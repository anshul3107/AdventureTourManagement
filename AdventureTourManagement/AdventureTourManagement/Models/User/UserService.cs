using AdventureTourManagement.Interface.User;
using AdventureTourManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureAccess;
using SecureAccess.Helper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AdventureTourManagement.Models
{
    public class UserService : IUser
    {
        private ATMDbContext _dbContext;
        private EncryptionDecryption _encryption;
        public UserService(ATMDbContext dbContext,IServiceProvider provider)
        {
            _dbContext = dbContext;
            var secureaccessFactory = new SecureAccessFactory();
            _encryption = secureaccessFactory.CreateInstance(provider).SecureAccess.GetEncryptionDecryption;
        }

        public async Task<VmUser> AddNewUser(VmUser userInput)
            {
            VmUser retUser = null;
            

           if(!string.IsNullOrEmpty( userInput.UserEmail) && !(string.IsNullOrEmpty(userInput.UserEncyryptedKey)))
            {
                var checkUserExists = await _dbContext.User.AsNoTracking().Where(x => x.user_email == userInput.UserEmail).FirstOrDefaultAsync();
                if (checkUserExists != null)
                {
                    retUser = new VmUser(checkUserExists);
                    retUser.Message = "User with same email id exists";
                    return retUser;
                }
                else
                {
                    var masterKey = userInput.UserEmail.Substring(2, 4);

                    User entity = new User()
                    {
                        user_email = userInput.UserEmail,
                        user_contact = userInput.UserContact,
                        user_encryptedkey = _encryption.EncryptText(userInput.UserEncyryptedKey, userInput.UserEncryptionMessage),
                        user_encryptedmessage = _encryption.EncryptText(userInput.UserEncryptionMessage, masterKey),
                    user_name = userInput.UserName
                    };

                    var execResult = await _dbContext.User.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();

                    if(execResult.Entity != null)
                    {
                        retUser = new VmUser(execResult.Entity);
                        retUser.Message = "User Added Successfully";
                        return retUser;
                    }

                }

            }
           else
            {
                retUser = new VmUser();
                retUser.Message = "User Email and password cannot be blank";
                return retUser;
            }

            return retUser;
        }

        public async Task<VmUser> GetUserProfile(string user_email)
        {
            var entity = await _dbContext.User.AsNoTracking().Where(x => x.user_email == user_email).FirstOrDefaultAsync();

            return new VmUser(entity);
        }

        public async Task ProvideFeedback(int activityId, int activityRating)
        {
            ActivityRatings rating = new ActivityRatings
            {
                activity_id = activityId,
                activity_rating = activityRating
            };

           await _dbContext.ActivityRatings.AddAsync(rating);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<VmUser> UpdatePassword(VmUser userDets)
        {
            VmUser result = null;
            
            if(!string.IsNullOrEmpty(userDets.UserEmail))
            {
                var existingUser = await _dbContext.User.AsNoTracking().Where(x => x.user_email == userDets.UserEmail).FirstOrDefaultAsync();
                var newKey = _encryption.EncryptText(userDets.UserEncyryptedKey, userDets.UserEncryptionMessage);
                var masterKey = userDets.UserEmail.Substring(2, 4);
                var newMessage = _encryption.EncryptText(userDets.UserEncryptionMessage, masterKey);
                existingUser.user_encryptedkey = newKey;
                existingUser.user_encryptedmessage = newMessage;
               var execResult =  _dbContext.User.Update(existingUser);
                await _dbContext.SaveChangesAsync();
                result = new VmUser(execResult.Entity);

            }

            return result;
        }

        public async Task<VmUser> UpdateUserDetails(VmUser userDets)
        {
            VmUser result = null;

            if (!string.IsNullOrEmpty(userDets.UserEmail))
            {
                var existingUser = await _dbContext.User.AsNoTracking().Where(x => x.user_email == userDets.UserEmail).FirstOrDefaultAsync();
                User entity = new User()
                {
                    id = existingUser.id,
                    user_email = existingUser.user_email,
                    user_contact = userDets.UserContact,
                    user_name = userDets.UserName,
                    user_encryptedkey = existingUser.user_encryptedkey,
                    user_encryptedmessage = existingUser.user_encryptedmessage,
                    modified_date = DateTime.Now
                };

                var execResult = _dbContext.User.Update(entity);
                await _dbContext.SaveChangesAsync();
                result = new VmUser(execResult.Entity);

            }

            return result;
        }

        public async Task<VmUser> ValidateUserLogin(VmUser userLogin)
        {
            VmUser vUser = null;

            if(!string.IsNullOrEmpty(userLogin.UserEmail) && !string.IsNullOrEmpty(userLogin.UserEncyryptedKey))
            {
                var userDetails = await _dbContext.User.AsNoTracking().Where(x => x.user_email == userLogin.UserEmail).FirstOrDefaultAsync();

                var masterKey = userDetails.user_email.Substring(2, 4);
                var message = _encryption.DecryptText(userDetails.user_encryptedmessage, masterKey);
                var newMessage = _encryption.EncryptText(userDetails.user_encryptedkey, message);
                var result = _encryption.CompareStrings(userDetails.user_encryptedkey, newMessage,message);

                if (result)
                {
                    vUser = new VmUser(userDetails);
                    return vUser;
                }
                else
                {
                    vUser = new VmUser();
                    vUser.Message = "Invali email/password";
                }
            }
            else
            {
                vUser = new VmUser();
                vUser.Message = "Invali email/password";
            }

            return vUser;

        }

        private string GenerateHash(string text)
        {
            byte[] hashbyte = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(hashbyte);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(hashbyte)}");

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashedValue = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: text,
                salt: hashbyte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedValue;
        }
    }
}
