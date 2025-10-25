using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Hearth.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string AccountId { get; set; }
        public string? Account_Owner { get; set; }
        public string? Authorized_Date { get; set; }
        public string? Authorized_Datetime { get; set; }
        // please work for hte love of god
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Plaid_Category_Id { get; set; }
        public string? Check_Number { get; set; }
        public string? Datetime { get; set; }
        public string Iso_Currency_Code { get; set; }
        public string? Logo_Url { get; set; }
        public string? Merchant_Entity_Id { get; set; }
        public string? Merchant_Name { get; set; }
        public string? Payment_Channel { get; set; }
        public bool Pending { get; set; }
        public string? Pending_Transaction_Id { get; set; }
        public string Personal_Finance_Category_Icon_Url { get; set; }
        public string? Transaction_Code { get; set; }
        public string? Transaction_Type { get; set; }
        public string? Unofficial_Currency_Code { get; set; }
        public string? Website { get; set; }
        public string? Personal_Finance_Category_Primary { get; set; }
        public string? Personal_Finance_Category_Detailed { get; set; }
        public string? Personal_Finance_Category_Confidence_Level { get; set; }


        // Not Mapped Props
        [NotMapped]
        public Account? Account { get; set; }
        // Below are varibales enterprited from Date
        [NotMapped]
        public int Year
        {
            get
            {
                if (DateTime.TryParseExact(Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate.Year;
                }
                throw new FormatException("Invalid date format. Expected 'YYYY-MM-DD'.");
            }
        }
        [NotMapped]
        public int Month
        {
            get
            {
                if (DateTime.TryParseExact(Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate.Month;
                }
                throw new FormatException("Invalid date format. Expected 'YYYY-MM-DD'.");
            }
        }
        [NotMapped]
        public int Day
        {
            get
            {
                if (DateTime.TryParseExact(Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate.Day;
                }
                throw new FormatException("Invalid date format. Expected 'YYYY-MM-DD'.");
            }
        }
    }

    public class Counterparty
    {
        public string ConfidenceLevel { get; set; }
        public string EntityId { get; set; }
        public string LogoUrl { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }
        public string Website { get; set; }
    }

    public class Location
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string StoreNumber { get; set; }
    }

    public class PaymentMeta
    {
        public string ByOrderOf { get; set; }
        public string Payee { get; set; }
        public string Payer { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentProcessor { get; set; }
        public string PpdId { get; set; }
        public string Reason { get; set; }
        public string ReferenceNumber { get; set; }
    }

    public class TransactionSyncResponse
    {
        [NotMapped]
        public List<Transaction> Added { get; set; }
        [NotMapped]
        public List<Transaction> Modified { get; set; }
        [NotMapped]
        public List<Transaction> Removed { get; set; }
        [NotMapped]
        public List<Account> Accounts { get; set; }
        public int Id { get; set; }
        public string Next_Cursor { get; set; }
        public bool Has_More { get; set; }
        public string Request_Id { get; set; }
        public string Transactions_Update_Status { get; set; }
        public string Write_Date { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
    }

    public class TransactionSyncRecord
    {
        public int Id { get; set; }
        public string Next_Cursor { get; set; }
        public bool Has_More { get; set; }
        public string Request_Id { get; set; }
        public string Transactions_Update_Status { get; set; }
        public DateTime Write_Date { get; set; } = DateTime.UtcNow;
        public string Bank_Id { get; set; }
    }

    public class TransactionFunctions
    {
        public static List<Transaction> GetAccountTransactions(HearthDbContext _dbContext, string accountId)
        {
            List<Transaction> result = new();

            result = _dbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .ToList();

            return result;
        }

        public static List<Transaction> GetAllUnignoredTransactions(HearthDbContext _dbContext)
        {
            List<Transaction> result = new();

            result = _dbContext.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .ToList();

            result = result
                .Where(t => t.Category == null || t.Category.Ignore == false)
                .ToList();



            return result;
        }

        public static List<Transaction> GetAllTransactions(HearthDbContext _dbContext)
        {
            List<Transaction> result = new();

            result = _dbContext.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.Date)
                .ToList();

            return result;
        }

        // This one is used by Plaid Call
        public static void AutoAssignCategories(List<Transaction> transactions, List<Category> categories, List<CategoryOrganizationRule> rules)
        {

            // Sets an array equal to the size of the Rules List (+1 because arrays start at 0)
            int[] scoreboard = new int[categories.Count() + 1];

            foreach (var t in transactions)
            {
                foreach (var r in rules)
                {

                    int points = 0;

                    if (r.Active)
                    {
                        // If Plaid Category Criteria matches
                        if (r.Plaid_Category == t.Personal_Finance_Category_Primary)
                        {
                            points++;
                        }
                        // If Merchant Name matches
                        if (r.Merchant_Name == t.Merchant_Name)
                        {
                            points++;
                        }
                        // If Amount  matches
                        if (r.Amount == t.Amount)
                        {
                            points++;
                        }

                        // Set Score
                        scoreboard[r.CategoryId] = points;
                    }
                    else
                    {
                        scoreboard[r.CategoryId] = 1;
                    }


                }

                // Find the winning Rule's Category
                int maxValue = scoreboard.Max();
                int maxIndex = Array.IndexOf(scoreboard, maxValue);
                var winningCategory = categories.SingleOrDefault(c => c.Id == maxIndex);

                if (winningCategory == null)
                {
                    // Sets it to the "Other" Category
                    winningCategory = categories[0];
                }

                // Set the winning Category
                t.CategoryId = winningCategory?.Id;
            }
        }
        // This one is used by the Categorization Page
        public static void AutoAssignAllCategories(HearthDbContext _dbContext)
        {
            var transactions = _dbContext.Transactions.ToList();
            var categories = _dbContext.Categories.ToList();
            var rules = _dbContext.CategoryOrganizationRules
                .Where(r => r.Active == true)
                .ToList();

            foreach (var t in transactions)
            {
                // Reset scoreboard per transaction
                var scoreboard = new Dictionary<int, int>();

                foreach (var r in rules)
                {
                    int points = 0;

                    // If a Plaid Category is specified in the rule
                    if (!string.IsNullOrEmpty(r.Plaid_Category))
                    {
                        // Add a point if it matches, subtract a point if it doesn't
                        points += (r.Plaid_Category == t.Personal_Finance_Category_Primary) ? 1 : -1;
                    }
                    // If a Merchant Name is specified in the rule
                    if (!string.IsNullOrEmpty(r.Merchant_Name))
                    {
                        // If (for some reason) the transaction's Merchant Name is null, subtract a point
                        if (t.Merchant_Name == null)
                        {
                            points += -1;
                            continue;
                        }
                        else
                        {
                            // Switches matching requirements based on operator
                            switch (r.Merchant_Name_Operator)
                            {
                                case STRING_OPERATOR.Contains:
                                    points += (t.Merchant_Name.Contains(r.Merchant_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;

                                case STRING_OPERATOR.StartsWith:
                                    points += (t.Merchant_Name.StartsWith(r.Merchant_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;

                                case STRING_OPERATOR.EndsWith:
                                    points += (t.Merchant_Name.EndsWith(r.Merchant_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;

                                case STRING_OPERATOR.Exact:
                                    points += (t.Merchant_Name.Equals(r.Merchant_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;
                            }
                        }
                    }

                    // If a Transaction Name is specified in the rule
                    if (!string.IsNullOrEmpty(r.Transaction_Name))
                    {
                        // If (for some reason) the transaction's Name is null, subtract a point
                        if (t.Name == null)
                        {
                            points += -1;
                            continue;
                        }
                        else
                        {
                            // Switches matching requirements based on operator
                            switch (r.Transaction_Name_Operator)
                            {
                                case STRING_OPERATOR.Contains:
                                    points += (t.Name.Contains(r.Transaction_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;

                                case STRING_OPERATOR.StartsWith:
                                    points += (t.Name.StartsWith(r.Transaction_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;

                                case STRING_OPERATOR.EndsWith:
                                    points += (t.Name.EndsWith(r.Transaction_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;

                                case STRING_OPERATOR.Exact:
                                    points += (t.Name.Equals(r.Transaction_Name, StringComparison.OrdinalIgnoreCase)) ? 1 : -1;
                                    break;
                            }
                        }
                    }
                    if (r.Amount != null)
                    {
                        points += (r.Amount == t.Amount) ? 1 : -1;
                    }

                    // Add or update scoreboard
                    if (scoreboard.ContainsKey(r.CategoryId))
                        scoreboard[r.CategoryId] += points;
                    else
                        scoreboard[r.CategoryId] = points;
                }

                // Pick the winning category
                if (scoreboard.Any())
                {
                    var best = scoreboard.OrderByDescending(kv => kv.Value).First();

                    // If best score is negative, fallback to "Other"
                    if (best.Value < 0)
                    {
                        t.CategoryId = categories.FirstOrDefault()?.Id; // assumes first = "Other"
                    }
                    else
                    {
                        var winningCategory = categories.SingleOrDefault(c => c.Id == best.Key)
                                            ?? categories.FirstOrDefault(); // fallback to "Other"

                        t.CategoryId = winningCategory?.Id;
                    }
                }
                else
                {
                    // no rules → fallback
                    t.CategoryId = categories.FirstOrDefault()?.Id;
                }
            }

            _dbContext.SaveChanges();
        }

    }

}


/*

"BANK_FEES":
"ENTERTAINMENT":
"FOOD_AND_DRINK":
"GENERAL_MERCHANDISE":
"GENERAL_SERVICES":
" GOVERNMENT_AND_NON_PROFIT":
"HOME_IMPROVEMENT":
"INCOME":
"LOAN_PAYMENTS":
"MEDICAL":
"RENT_AND_UTILITIES":
"TRANSFER_IN":
"TRANSFER_OUT":
"TRANSPORTATION":
"TRAVEL":
 
*/