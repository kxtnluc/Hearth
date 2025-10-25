using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearth.Models;
using Hearth.Services;

namespace Hearth.Components.Pages.Finances.subpages
{
    public partial class AccountsPage : ComponentBase
    {

        private bool initialRenderComplete = false;
        // This is hard coded for now, but will be selected in a dropdown later on.
        private string bankId = "XzJ5Evn0p8skrzndg7zeCQAE1YkypVH4Obp6v";

        private List<Account> accounts = new List<Account>();

        public User? activeUser { get; set; }
        protected override async Task OnInitializedAsync()
        {
            // For some reason "GetActiveUserData" HAS to be in the .razor file. it CANNOT be in .razor.cs
            activeUser = await UserFunctions.GetActiveUserData();

            accounts = AccountFunctions.GetUserAccounts(_dbContext, activeUser.Id);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (initialRenderComplete == false)
                {
                    initialRenderComplete = true;
                    StateHasChanged(); // Trigger re-render to hide the spinner
                }
            }
        }

        //private async void HandleBankButton()
        //{
        //    if (activeUser == null || bankId == null) return;
        //    await _plaidService.CreateAndUpdateAccounts(bankId, activeUser.Id)!;
        //}
    }
}