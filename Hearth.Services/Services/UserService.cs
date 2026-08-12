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
        private const string SessionTokenKey = "session_token";

        public UserService(
            HearthDbContext context,
            ISecureStorageProvider secureStorage
        ) 
        : base(context)
        {
            _secureStorage = secureStorage;
        }
        #region Abstract Class Setup
        protected override DbSet<User> DbSet => _context.Users;
        protected override UserDTO ToDto(User entity) => entity.ToDto();
        protected override User ToEntity(UserDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(UserDTO dto, User entity) => dto.ApplyUpdate(entity);
        #endregion

        #region Model Specific Functions
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
