using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.UI.Models.Plaid;
public class PlaidLinkResult
{
    public bool Success { get; set; }
    public string? AccessToken { get; set; }
    public string? ErrorMessage { get; set; }
}
