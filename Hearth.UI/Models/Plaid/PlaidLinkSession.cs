using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.UI.Models.Plaid
{
    internal class PlaidLinkSession
    {
        private readonly TaskCompletionSource<(bool Success, string? Data)> _tcs = new();

        public Task<(bool Success, string? Data)> Completion => _tcs.Task;

        [JSInvokable]
        public Task OnSuccess(string publicToken)
        {
            _tcs.TrySetResult((true, publicToken));
            return Task.CompletedTask;
        }

        [JSInvokable]
        public Task OnError(string errorDetails)
        {
            _tcs.TrySetResult((false, errorDetails));
            return Task.CompletedTask;
        }
    }
}
