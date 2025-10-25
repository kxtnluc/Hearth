using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearth.Services;
using Hearth.Data;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Models.CTO
{
    public class ReportCTO
    {
        #region Set Variables
        public List<Transaction> Transactions { get; set; }
        public List<Transaction> Previous_Transactions { get; set; }
        public DateTime Real_From { get; set; } // The Date of the Earliest Transaction on the list
        public DateTime Real_To { get; set; } // The Date of the Latest Transaction on the list
        public DateTime? From { get; set; } // The Date Passed to collect the transactions from
        public DateTime? To { get; set; } // The Date Passed to collect the transactions to
        public bool Is_Primary { get; set; } = false; // used to show that this is, or is not a Previous Transaction
        #endregion /Set Variables
        #region Calculated Variables
        public decimal Expenses_C
        {
            get
            {
                var transactions = Transactions;
                return transactions != null && transactions.Count != 0
                    ? transactions.Where(t => t.Amount > 0).Sum(t => t.Amount)
                    : 0;
            }
        }
        public Transaction Largest_Expense_C
        {
            get
            {
                var transactions = Transactions;
                return transactions != null && transactions.Count != 0
                    ? transactions.OrderByDescending(t => t.Amount).FirstOrDefault()
                    : new Transaction();
            }
        }
        public decimal Income_C
        {
            get
            {
                decimal result = 0;
                var transactions = Transactions;

                if (transactions != null && transactions.Count != 0)
                {
                    result = Math.Abs(transactions.Where(t => t.Amount < 0).Sum(t => t.Amount));
                }

                return result;
            }
        }
        public decimal Profit_C
        {
            get
            {
                var income = Income_C;
                var expenses = Expenses_C;
                var profit = (income - expenses);
                return profit;
            }
        }
        public decimal Percent_Expenses_Of_Income_C
        {
            get
            {
                decimal rate = 0;
                var expenses = Expenses_C;
                var income = Income_C;

                if (income == 0)
                {
                    return rate;
                }

                rate = Math.Round(((expenses / income) * 100), 2);

                return rate;
            }
        }

        public decimal Percent_Profit_Of_Income_C
        {
            get
            {
                decimal rate = 0;
                var profit = Profit_C;
                var income = Income_C;

                if (income == 0)
                {
                    return rate;
                }

                rate = Math.Round(((profit / income) * 100), 2);

                return rate;
            }
        }
        public decimal Median_C
        {
            get
            {
                var transactions = Transactions;
                var median = CalculateMedian(transactions);
                return median;
            }
        }
        public decimal Mode_C
        {
            get
            {
                var transactions = Transactions;
                var mode = CalculateMode(transactions);
                return mode;
            }
        }
        public decimal Range_C
        {
            get
            {
                var transactions = Transactions;
                var range = CalculateRange(transactions);
                return range;
            }
        }

        public decimal Standard_Deviation_Sample_C
        {
            get
            {
                var transactions = Transactions;
                var sds = CalculateStandardDeviation(transactions);
                return sds;
            }
        }

        public decimal Standard_Deviation_Population_C
        {
            get
            {
                var transactions = Transactions;
                var sdp = CalculateStandardDeviation(transactions, false);
                return sdp;
            }
        }

        public decimal Average_Spending_Per_Day_C
        {
            get
            {

                var transactions = Transactions;

                if(Real_Number_Of_Days_C == 0)
                {
                    return transactions.Sum(t => t.Amount);
                }

                return transactions != null && transactions.Count != 0
                    ? Math.Round(transactions.Where(t => t.Amount > 0).Sum(t => t.Amount) / (decimal)Real_Number_Of_Days_C, 2)
                    : 0;
            }
        }
        public double Real_Number_Of_Days_C
        {
            get
            {
                return (Real_To - Real_From).TotalDays;
            }
        }
        public double Number_Of_Days_C
        {
            get
            {
                if (To.HasValue && From.HasValue)
                {
                    var to = To.Value;
                    var from = From.Value;
                    return (to - from).TotalDays;
                }
                else
                {
                    return 0;
                }
            }
        }
        public List<CategorySpending> Category_Spending_Map
        {
            get
            {
                var transactions = Transactions;
                var result = new List<CategorySpending>();

                result = transactions
                    .GroupBy(t => t.CategoryId)
                    .Select(g => new CategorySpending
                    {
                        CategoryId = g.Key,
                        TotalAmount = g.Sum(t => t.Amount),       // or use (int) if Amount is decimal
                        TransactionCount = g.Count()
                    })
                    .ToList();

                return result;
            }
        }
        #endregion /Calculated Variables
        #region Compared_Report
        public ReportCTO Compared_Report { get; set; }

        #endregion Compared_Report
        #region Comparison Variables
        public decimal Comp_Expenses_Num_C
        {
            get
            {
                var previous = Compared_Report;
                return previous != null && previous.Expenses_C != 0
                    ? (Expenses_C - previous.Expenses_C)
                    : 0;
            }
        }
        public decimal Comp_Expenses_Percent_C
        {
            get
            {
                var previous = Compared_Report;
                return previous != null && previous.Expenses_C != 0
                    ? Math.Round(((Expenses_C - previous.Expenses_C) / previous.Expenses_C) * 100, 2)
                    : 0;
            }
        }
        public decimal Comp_Income_Num_C
        {
            get
            {
                var previous = Compared_Report;
                return previous != null && previous.Income_C != 0
                    ? (Income_C - previous.Income_C)
                    : 0;
            }
        }
        public decimal Comp_Income_Percent_C
        {
            get
            {
                var previous = Compared_Report;
                return previous != null && previous.Income_C != 0
                    ? Math.Round(((Income_C - previous.Income_C) / previous.Income_C) * 100, 2)
                    : 0;

            }
        }
        #endregion /Comparison Variables
        #region Projection Variables
        public decimal Proj_Expenses_C
        {
            get
            {
                return (Expenses_C * Comp_Expenses_Percent_C) + Expenses_C;
            }
        }
        public decimal Proj_Income_C
        {
            get
            {
                return (Income_C * Comp_Income_Percent_C) + Income_C;
            }
        }
        #endregion /Projection Variables

        // This function below was mostly generated by AI so check to make sure it works properly
        private decimal CalculateMedian(List<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
                return 0;

            var sortedAmounts = transactions
                .Where(t => t.Amount > 0)
                .Select(t => t.Amount)
                .OrderBy(amount => amount)
                .ToList();

            int count = sortedAmounts.Count;
            int middle = count / 2;

            if (count % 2 == 0)
            {
                // Even number of elements → average of two middle values
                return (sortedAmounts[middle - 1] + sortedAmounts[middle]) / 2;
            }
            else
            {
                // Odd number → middle value
                return sortedAmounts[middle];
            }
        }

        private decimal CalculateMode(List<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
                return 0;

            var amountFrequencies = transactions
                .Where(t => t.Amount > 0)
                .GroupBy(t => t.Amount)
                .ToDictionary(g => g.Key, g => g.Count());

            int maxFrequency = amountFrequencies.Values.Max();

            // Get all values with the max frequency
            var modes = amountFrequencies
                .Where(pair => pair.Value == maxFrequency)
                .Select(pair => pair.Key)
                .ToList();

            // If there's a tie for mode, return the smallest mode value (or handle as needed)
            return modes.First(); // Or modes.Min() if you prefer the smallest
        }

        private decimal CalculateRange(List<Transaction> transactions)
        {
            if (transactions == null || transactions.Count == 0)
                return 0;

            var amounts = transactions
                .Where(t => t.Amount > 0)
                .Select(t => t.Amount);

            decimal min = amounts.Min();
            decimal max = amounts.Max();

            return max - min;
        }

        private decimal CalculateStandardDeviation(List<Transaction> transactions, bool isSample = true)
        {
            if (transactions == null || transactions.Count == 0)
                return 0;

            var amounts = transactions
                .Where(t => t.Amount > 0)
                .Select(t => t.Amount)
                .ToList();
            decimal mean = amounts.Average();

            decimal sumOfSquares = amounts
                .Select(amount => (amount - mean) * (amount - mean))
                .Sum();

            decimal variance;

            if (isSample)
            {
                variance = sumOfSquares / (amounts.Count - 1); // For sample standard deviation
            }
            else
            {
                variance = sumOfSquares / amounts.Count; // For population standard deviation
            }

            decimal standardDeviation = (decimal)Math.Sqrt((double)variance);
            return Math.Round(standardDeviation, 2);
        }

    }
    public class CategorySpending
    {
        public int? CategoryId { get; set; }
        public decimal TotalAmount { get; set; }
        public int TransactionCount { get; set; }
    }

}
