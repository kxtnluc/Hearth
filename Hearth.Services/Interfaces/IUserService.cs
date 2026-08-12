using Hearth.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    public interface IUserService : ISqliteTableService<UserDTO>
    {
        /// <summary>
        /// Checks if there is a user currently logged in by checking for a stored session token.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsLoggedIn();
        /// <summary>
        /// Checks if there is a user currently logged in and returns the current user's information if available.
        /// </summary>
        /// <returns></returns>
        Task<UserDTO?> GetCurrentUser();
        /// <summary>
        /// Attempts to log in a user with the provided username and password. If successful, it stores a session token for future authentication checks.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Login(string username, string password);
        /// <summary>
        /// Removes the session token assosiated with the user, effectively logging them out of the application.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> Logout(UserDTO user);
        /// <summary>
        /// Tries to find a User via the provided username. Returns null if no user is found.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<UserDTO?> GetByUsername(string username);
    }
}
