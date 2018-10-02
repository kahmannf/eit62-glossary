using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public interface IUserBusiness
    {
        /// <summary>
        /// Gets a user by the guid
        /// </summary>
        /// <param name="guid">Global Unique Identifier of the user</param>
        /// <returns>The user associated with the guid, null if non exists</returns>
        Task<User> GetUser(string guid);
        /// <summary>
        /// Saves a User
        /// </summary>
        /// <param name="user">The user Object to save</param>
        /// <returns>The Users guid</returns>
        Task<bool> SaveUser(User user);
        /// <summary>
        /// Creates a new User and sends a registration email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> CreateUser(string email, string verifyBaseUrl);
        /// <summary>
        /// Activates a user account
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="password"></param>
        /// <param name="verificationKey"></param>
        /// <returns></returns>
        Task<bool> VerifyUser(byte[] password, string verificationKey);
        /// <summary>
        /// Updates the password for the user
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(string verificationKey, byte[] oldPassword, byte[] newPassword);
        /// <summary>
        /// Resets a users password and sends a password reset email id user for email exists
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(string email);
        Task<bool> CheckLoginData(string email, byte[] password);
    }
}
