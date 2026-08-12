using Hearth.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    public interface IUserService : ISqliteTableService<UserDTO>
    {
        Task<bool> IsLoggedIn();
        Task<UserDTO?> GetCurrentUser();
    }
}
