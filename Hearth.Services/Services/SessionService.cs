using Hearth.Core.Data;
using Hearth.Core.Models;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs;
using Hearth.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Services
{
    public class SessionService : ISessionService
    {
        //public SessionService(HearthDbContext context) : base(context) { }
        public string GenerateSessionToken(User user)
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
