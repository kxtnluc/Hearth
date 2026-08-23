using Hearth.UI.Models.Plaid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.UI.Interfaces.Plaid;

public interface IPlaidLinkService
{
    Task<PlaidLinkResult> LinkBankAccount(int userId);
}
