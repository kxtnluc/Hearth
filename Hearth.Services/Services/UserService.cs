using Hearth.Core.Data;
using Hearth.Core.Models;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs;
using Hearth.Services.Interfaces;
using Hearth.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services
{
    public class UserService : ASqliteTableService<User, UserDTO>, IUserService
    {
        private readonly ISecureStorageProvider _secureStorage;
        private readonly ISessionService _sessionService;
        private const string SessionTokenKey = "session_token";

        public UserService(
            HearthDbContext context,
            ISecureStorageProvider secureStorage,
            ISessionService sessionService
        ) 
        : base(context)
        {
            _secureStorage = secureStorage;
            _sessionService = sessionService;
        }
        #region Abstract Class Setup
        protected override DbSet<User> DbSet => _context.Users;
        protected override UserDTO ToDto(User entity) => entity.ToDto();
        protected override User ToEntity(UserDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(UserDTO dto, User entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(UserDTO payload)
        {
            if(payload.Name == null || payload.Pin == null)
            {
                throw new ArgumentException("UserDTO must have a Name and Pin.");
            }

            return;
        }
        #endregion

        #region Model Specific Functions
        public async Task<UserDTO?> GetByUsername(string username)
        {
            var user = await DbSet.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null) return null;

            return user?.ToDto();
        }
        public async Task<bool> Login(string username, string password)
        {
            UserDTO? user = await GetByUsername(username);

            if (user == null)
            {
                return false;
            }
            // TODO: Need to do a better encryption or something for this
            if (user.Pin != password)
            {
                return false;
            }

            // Generate a session token
            var sessionToken = _sessionService.GenerateSessionToken(user);
            // Add it to secure storage for future authentication checks
            await _secureStorage.SetAsync(SessionTokenKey, sessionToken);

            return true;
        }
        public async Task<bool> Logout(UserDTO user)
        {
            // Remove the session token from secure storage
            _secureStorage.Remove(SessionTokenKey);
            return true;
        }
        public async Task<bool> IsLoggedIn()
        {
            var storedToken = await _secureStorage.GetAsync(SessionTokenKey);
            return !string.IsNullOrEmpty(storedToken);
        }

        public async Task<UserDTO?> GetCurrentUser()
        {
            var storedToken = await _secureStorage.GetAsync(SessionTokenKey);
            if (string.IsNullOrEmpty(storedToken)) return null;

            // TODO: resolve the token to a real user — depends on how you're
            // structuring auth (local PIN, session table, etc.)
            // e.g.: return await GetById(userIdFromToken);
            return null;
        }
        #endregion
    }
}
