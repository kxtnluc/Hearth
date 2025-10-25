using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models.CTO
{
    public class NetWorthCTO
    {
        #region Set Variables
        public List<Account> Accounts { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();
        #endregion

        #region Calculated Variables
        public decimal NetWorth_C
        {
            get
            {
                var result = 0.00M;
                result = Assets_C - Liabilities_C;
                return result;
            }
        }
        public decimal Assets_C
        {
            get
            {
                var result = 0.00M;
                result = (A_Liquid_C + A_Investments_C + A_Fixed_C);
                return result;
            }
        }

        public decimal Liabilities_C
        {
            get
            {
                var result = 0.00M;
                result = (L_Loans_C + L_Debt_C + L_Mortgage_C);
                return result;
            }
        }

        public decimal A_Liquid_C
        {
            get
            {
                var result = 0.00M;
                // this might need to be fixed later
                result = (decimal)Accounts.Sum(a => a.Balance_Current);
                return result;
            }
        }

        public decimal A_Investments_C
        {
            get
            {
                var result = 0.00M;
                // Calculate total investments here
                return result;
            }
        }

        public decimal A_Fixed_C
        {
            get
            {
                var result = 0.00M;
                // Calculate total fixed assets here
                return result;
            }
        }

        public decimal L_Mortgage_C
        {
            get
            {
                var result = 0.00M;
                // Calculate total mortgage here
                return result;
            }
        }

        public decimal L_Loans_C
        {
            get
            {
                var result = 0.00M;
                // Calculate total loans here
                return result;
            }
        }

        public decimal L_Debt_C
        {
            get
            {
                var result = 0.00M;
                // Calculate total debt here
                return result;
            }
        }




















        // Average Calculations
        public decimal Avg_Monthly_Income_C
        {
            get
            {
                var result = 0.00M;

                var incomeTransactions = Transactions.Where(t => t.Amount < 0);
                var numberOfMonths = 0;

                if (incomeTransactions.Any())
                {
                    // Find earliest transaction date
                    var earliest = incomeTransactions
                        .Select(t => DateTime.ParseExact(t.Date, "yyyy-MM-dd", null))
                        .Min();

                    // Find how many months between earliest and now (inclusive)
                    var now = DateTime.Now;
                    numberOfMonths = ((now.Year - earliest.Year) * 12 + now.Month - earliest.Month) + 1;
                }
                else
                {
                    result = 0;
                    return result;
                }

                var total = Math.Abs(incomeTransactions.Sum(t => t.Amount));
                result = Math.Abs(total / (numberOfMonths == 0 ? 1 : numberOfMonths));
                result = Math.Round(result);
                return result;
            }
        }
        public decimal Avg_Monthly_Expenses_C
        {
            get
            {
                var result = 0.00M;

                var incomeTransactions = Transactions.Where(t => t.Amount > 0);
                var numberOfMonths = 0;

                if (incomeTransactions.Any())
                {
                    // Find earliest transaction date
                    var earliest = incomeTransactions
                        .Select(t => DateTime.ParseExact(t.Date, "yyyy-MM-dd", null))
                        .Min();

                    // Find how many months between earliest and now (inclusive)
                    var now = DateTime.Now;
                    numberOfMonths = ((now.Year - earliest.Year) * 12 + now.Month - earliest.Month) + 1;
                }
                else
                {
                    result = 0;
                    return result;
                }

                var total = Math.Abs(incomeTransactions.Sum(t => t.Amount));
                result = Math.Abs(total / (numberOfMonths == 0 ? 1 : numberOfMonths));
                result = Math.Round(result);
                return result;
            }
        }
        public decimal Avg_Monthly_Profit_C
        {
            get
            {
                var result = 0.00M;
                result = Avg_Monthly_Income_C - Avg_Monthly_Expenses_C;
                return result;
            }
        }

        public decimal Percent_Income_Change_From_Past_Month_C
        {
            get
            {
                if (Transactions == null || !Transactions.Any())
                    return 0;

                // Parse transaction dates and filter income only
                var incomeTransactions = Transactions
                    .Where(t => t.Amount < 0)
                    .Select(t => new
                    {
                        Date = DateTime.ParseExact(t.Date, "yyyy-MM-dd", null),
                        t.Amount
                    })
                    .ToList();

                if (!incomeTransactions.Any())
                    return 0;

                // Get current month and year
                var now = DateTime.Now;
                var currentMonth = now.Month;
                var currentYear = now.Year;

                // Transactions for *this month*
                var currentMonthIncome = incomeTransactions
                    .Where(t => t.Date.Month == currentMonth && t.Date.Year == currentYear)
                    .Sum(t => t.Amount);

                // Transactions for *all prior months*
                var pastMonthIncomes = incomeTransactions
                    .Where(t => t.Date < new DateTime(currentYear, currentMonth, 1))
                    .GroupBy(t => new { t.Date.Year, t.Date.Month })
                    .Select(g => g.Sum(x => x.Amount))
                    .ToList();

                // If no past months exist, just return 0 change
                if (!pastMonthIncomes.Any())
                    return 0;

                // Average of all previous months
                var avgIncomePast = pastMonthIncomes.Average();

                // Percent change formula:
                // ex: 1,200 divided by 3,500 = 0.342857. So current income is 34.29% of past average income, and thus a 65.71% decrease.
                var percentChange = (Math.Abs(currentMonthIncome) / Math.Abs(avgIncomePast));
                // To get that -65.71% value we subtract from 1. That way if more income was made in the current month, the value would be positive.
                percentChange = (percentChange - 1);

                // Round to 2 decimals for clarity
                var result = Math.Round(percentChange, 2);
                return result;
            }
        }

        public decimal Percent_Expenses_Change_From_Past_Month_C
        {
            get
            {
                if (Transactions == null || !Transactions.Any())
                    return 0;

                // Parse transaction dates and filter income only
                var expensesTransactions = Transactions
                    .Where(t => t.Amount > 0)
                    .Select(t => new
                    {
                        Date = DateTime.ParseExact(t.Date, "yyyy-MM-dd", null),
                        t.Amount
                    })
                    .ToList();

                if (!expensesTransactions.Any())
                    return 0;

                // Get current month and year
                var now = DateTime.Now;
                var currentMonth = now.Month;
                var currentYear = now.Year;

                // Transactions for *this month*
                var currentMonthExpense = expensesTransactions
                    .Where(t => t.Date.Month == currentMonth && t.Date.Year == currentYear)
                    .Sum(t => t.Amount);

                // Transactions for *all prior months*
                var pastMonthExpenses = expensesTransactions
                    .Where(t => t.Date < new DateTime(currentYear, currentMonth, 1))
                    .GroupBy(t => new { t.Date.Year, t.Date.Month })
                    .Select(g => g.Sum(x => x.Amount))
                    .ToList();

                // If no past months exist, just return 0 change
                if (!pastMonthExpenses.Any())
                    return 0;

                // Average of all previous months
                var avgExpensePast = pastMonthExpenses.Average();

                // Percent change formula:
                // ex: 1,200 divided by 3,500 = 0.342857. So current income is 34.29% of past average income, and thus a 65.71% decrease.
                var percentChange = (Math.Abs(currentMonthExpense) / Math.Abs(avgExpensePast));
                // To get that -65.71% value we subtract from 1. That way if more income was made in the current month, the value would be positive.
                percentChange = (percentChange - 1);

                // Round to 2 decimals for clarity
                var result = Math.Round(percentChange, 2);
                return result;

            }
        }

        public decimal Percent_Profit_Change_From_Past_Month_C
        {
            get
            {
                var result = 0M;
                result = Percent_Income_Change_From_Past_Month_C - Percent_Expenses_Change_From_Past_Month_C;
                return result;
            }
        }

        // Median Calculations
        public decimal Median_Monthly_Income
        {
            get
            {
                var result = 0.00M;
                // Calculate average monthly income here
                return result;
            }
        }
        public decimal Median_Monthly_Expenses
        {
            get
            {
                var result = 0.00M;
                // Calculate average monthly expenses here
                return result;
            }
        }
        public decimal Median_Monthly_Profit
        {
            get
            {
                var result = 0.00M;
                result = Median_Monthly_Income - Median_Monthly_Expenses;
                return result;
            }
        }
        #endregion
    }
}
