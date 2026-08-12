using Hearth.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    // TODO: later on, we will store sessions in the db so we can track who logged into the app
    public interface ISessionService //: ISqliteTableService<User> 
    {
        string GenerateSessionToken(User user);
    }
}
