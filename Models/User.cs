using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pin { get; set; }
        public int RoleId { get; set; } = 4;
        [NotMapped]
        public Role Role { get; set; }
    }

    public class UserFunctions
    {
        public static async Task<bool> IsUserLoggedIn()
        {
            // Retrieve the session token from secure storage
            var storedToken = "";

            try
            {
                storedToken = await SecureStorage.GetAsync("session_token");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An Error occured while fetching your login token: ", ex);
            }
            // Check if a token exists
            if (string.IsNullOrEmpty(storedToken))
            {
                // No token found, user is not logged in
                return false;
            }

            return true; // Token exists, consider the user as logged in
        }

        public static async Task<User> GetActiveUserData()
        {
            var isLoggedIn = await IsUserLoggedIn();

            if (isLoggedIn)
            {
                var storedToken = await SecureStorage.GetAsync("session_token");

                User user = new User { };

                // Split the string by '_'
                var parts = storedToken.Split('_');

                user.Name = parts[3];
                user.Id = int.Parse(parts[2]);
                user.RoleId = int.Parse(parts[4]);

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
