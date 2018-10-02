using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryInMemoryBusiness
{
    class UserBusiness : IUserBusiness
    {
        private FileHandler<User> _fileHandler = new FileHandler<User>(Path.Combine(Config.GetInstance().DataDirectory ?? throw new MissingConfigException("DataDirectory"), "user"));

        public Task<bool> CheckLoginData(string email, byte[] password)
        {
            if(_fileHandler.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()) is User user)
            {
                return Task.FromResult(Security.ComparePasswords(password, user.Hash, user.Salt));
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public async Task<bool> CreateUser(string email, string verifyBaseUrl)
        {
            try
            {
                if (_fileHandler.Any(x => x.Email.ToLower() == email.ToLower()))
                {
                    return false;
                }

                User user = new User()
                {
                    Guid = Guid.NewGuid().ToString(),
                    Email = email,
                    VerificationKey = Guid.NewGuid().ToString()
                };

                bool saved = await _fileHandler.Save(user);

                if(!saved)
                {
                    return false;
                }
                else
                {
                    EmailSender.SendVerificationEmail(user);
                    return true;
                }
            }
            catch(Exception ex)
            {
                ExceptionHandler.Notify(ex);
                return false;
            }
        }

        public Task<User> GetUser(string guid)
        {
            return Task.FromResult(_fileHandler.GetByGuid(guid));
        }

        public async Task<bool> ResetPassword(string email)
        {
            if(_fileHandler.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()) is User user)
            {
                user.VerificationKey = Guid.NewGuid().ToString();

                if(await _fileHandler.Save(user))
                {
                    EmailSender.SendPasswordResetEmail(user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
                
        }

        public Task<bool> SaveUser(User user)
        {
            try
            {
                return _fileHandler.Save(user);
            }
            catch (Exception ex)
            {
                ExceptionHandler.Notify(ex);
                return Task.FromResult(false);
            }
        }

        public async Task<bool> UpdatePassword(string verificationKey, byte[] oldPassword, byte[] newPassword)
        {
            try
            {
                if(_fileHandler.FirstOrDefault(x => x.VerificationKey == verificationKey) is User user 
                 && Security.ComparePasswords(oldPassword, user.Hash ?? new byte[0], user.Salt ?? new byte[0]))
                {
                    Security.CreateHashSalt(newPassword, out byte[] hash, out byte[] salt);
                    user.Hash = hash;
                    user.Salt = salt;
                    user.VerificationKey = null;
                    return await _fileHandler.Save(user);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Notify(ex);
                return false;
            }
        }

        public async Task<bool> VerifyUser(byte[] password, string verificationKey)
        {
            try
            {
                if (_fileHandler.FirstOrDefault(x => x.VerificationKey == verificationKey) is User user)
                {
                    Security.CreateHashSalt(password, out byte[] hash, out byte[] salt);
                    user.Hash = hash;
                    user.Salt = salt;
                    user.VerificationKey = null;
                    return await _fileHandler.Save(user);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Notify(ex);
                return false;
            }
        }
    }
}
