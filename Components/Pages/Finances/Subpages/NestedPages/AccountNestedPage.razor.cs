using Microsoft.AspNetCore.Components;
using Hearth.Models;

namespace Hearth.Components.Pages.Finances.Subpages.NestedPages
{
    public partial class AccountNestedPage
    {
        [Parameter]
        public string id { get; set; }
        public Account account;
        public List<Transaction> transactions;

        private string inputAccountName = string.Empty;
        private string inputAccountNumber = string.Empty;
        private string inputBankName = string.Empty;
        private bool inputIsOpen = true;

        protected override async Task OnInitializedAsync()
        {
            account = AccountFunctions.GetAccount(_dbContext, id);
            transactions = TransactionFunctions.GetAccountTransactions(_dbContext, id);
            AssignInputValues(account);
        }

        private bool showEditModal = false;
        private void HandleManageBtn()
        {
            OpenEditModal();
        }

        private void OpenEditModal()
        {
            showEditModal = true;
        }

        private void CloseEditModal()
        {
            showEditModal = false;
        }

        private void AssignInputValues(Account account)
        {
            if(account != null)
            {
                inputAccountName = account.Name;
                inputAccountNumber = account.Account_Number;
                inputBankName = account.Institution_Name;
                inputIsOpen = account.Is_Open ?? true;
            }
        }

        private void HandleSaveAccountChanges()
        {
            // Checks to see if changes were made
            if 
            (
                account.Name == inputAccountName &&
                account.Account_Number == inputAccountNumber &&
                account.Institution_Name == inputBankName &&
                account.Is_Open == inputIsOpen
            )
            {
                // No changes were made
                CloseEditModal();
                return;
            }

            // Creates an Account Object with updated values
            Account updatedAccount = new()
            {
                Id = account.Id, 
                Name = inputAccountName,
                Account_Number = inputAccountNumber,
                Institution_Name = inputBankName,
                Is_Open = inputIsOpen,
            };

            // Calls update function
            // TODO: Error Handling
            AccountFunctions.UpdateAccount(_dbContext, updatedAccount);

            // Closes Modal and returns.
            CloseEditModal();
            return;
        }
    }
}