using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Hearth.Models;

namespace Hearth.Services
{
    public class SessionService
    {
        public static string GenerateSessionToken(User user)
        {
            // Generate a unique identifier
            var guid = Guid.NewGuid().ToString();

            // Get the current timestamp
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff");

            // Combine the GUID and timestamp and UserId
            var rawToken = $"{guid}_{timestamp}_{user.Id}_{user.Name}_{user.RoleId}";

            // Hash the combined string using SHA256
            //using (var sha256 = SHA256.Create())
            //{
            //    var bytes = Encoding.UTF8.GetBytes(rawToken);
            //    var hash = sha256.ComputeHash(bytes);

            //    // Convert the hash to a hexadecimal string
            //    return BitConverter.ToString(hash).Replace("-", "");
            //}

            return rawToken;
        }
    }
}
