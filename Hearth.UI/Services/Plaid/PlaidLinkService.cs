using Hearth.Integrations.APIs.Plaid.Interfaces;
using Hearth.UI.Interfaces.Plaid;
using Hearth.UI.Models.Plaid;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.UI.Services.Plaid;

internal class PlaidLinkService : IPlaidLinkService
{
    private readonly IJSRuntime _js;
    private readonly IPlaidService _plaidService;

    public PlaidLinkService(IJSRuntime js, IPlaidService plaidService)
    {
        _js = js;
        _plaidService = plaidService;
    }

    public async Task<PlaidLinkResult> LinkBankAccount(int userId)
    {
        var linkToken = await _plaidService.CreateLinkTokenAsync(userId);
        if (string.IsNullOrEmpty(linkToken))
        {
            return new PlaidLinkResult { Success = false, ErrorMessage = "Failed to create link token." };
        }

        var session = new PlaidLinkSession();
        using var dotNetRef = DotNetObjectReference.Create(session);

        await _js.InvokeVoidAsync("openPlaidLinkModal", linkToken, dotNetRef);

        var (success, data) = await session.Completion;

        if (!success)
        {
            return new PlaidLinkResult { Success = false, ErrorMessage = data };
        }

        var bank = await _plaidService.StoreAccessTokenAsync(data!, userId);

        return new PlaidLinkResult
        {
            Success = bank is not null,
            AccessToken = bank?.Access_Token,
            ErrorMessage = bank is null ? "Something went wrong linking your bank." : null
        };
    }
}
