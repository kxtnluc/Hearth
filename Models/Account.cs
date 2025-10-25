using Hearth.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hearth.Models.Joins;
namespace Hearth.Models
{
    public class Account
    {
        [Required]
        public string Id { get; set; }
        public string BankId { get; set; }
        [NotMapped]
        [ForeignKey("BankId")]
        public Bank Bank { get; set; }
        public string? Mask { get; set; }
        public string? Name { get; set; }
        public string? Offical_Name { get; set; }
        public string? Type { get; set; }
        public string? Subtype { get; set; }
        public int UserId { get; set; }
        public string? Institution_Id { get; set; }
        public string? Institution_Name { get; set; }
        public string? Item_Id { get; set; }
        public string? Request_Id { get; set; }
        public DateTime? Inital_Date_Requested { get; set; }
        public DateTime? Last_Date_Requested { get; set; }
        public DateTime? Last_Modified { get; set; }
        public decimal? Balance_Available { get; set; }
        public decimal? Balance_Current { get; set; }

        [NotMapped]
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        // Properties Not Fetched by Plaid
        public string? Account_Number { get; set; }
        public bool? Is_Open { get; set; }
    }

    public class AccountFunctions
    {
        public static List<Account> GetBankAccounts(HearthDbContext _dbContext, string bankId)
        {
            List<Account> result;

            result = _dbContext.Accounts
                .Where(a => a.BankId == bankId)
                .ToList();

            return result;
        }

        public static List<Account> GetUserAccounts(HearthDbContext _dbContext, int userId)
        {
            // This is not well programed and should prob be redone later
            List<Account> result;

            var joined = _dbContext.Accounts
                .Where(a => a.UserId == userId)
                .Join(
                    _dbContext.Banks,
                    account => account.BankId,
                    bank => bank.Id,
                    (a, b) => new { Account = a, Bank = b })
                .ToList();

            foreach (var item in joined)
            {
                item.Account.Bank = item.Bank;
            }

            result = joined.Select(x => x.Account).ToList();

            return result;
        }

        public static Account GetAccount(HearthDbContext _dbContext, string id)
        {
            Account result;

            result = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == id)!;

            if (result == null) return new Account();

            return result;
        }

        public static void UpdateAccount(HearthDbContext _dbContext, Account account)
        {
            // Search for passed account in DB
            var foundAccount = _dbContext.Accounts
                .FirstOrDefault(a => a.Id == account.Id);

            // If not account is found, update it
            if (foundAccount != null)
            {
                foundAccount.Name = account.Name;
                foundAccount.Account_Number = account.Account_Number;
                foundAccount.Institution_Name = account.Institution_Name;
                foundAccount.Is_Open = account.Is_Open;
                foundAccount.Last_Modified = DateTime.Now;

                // Save changes to DB
                _dbContext.SaveChanges();
            }
            return;
        }
    }
}
