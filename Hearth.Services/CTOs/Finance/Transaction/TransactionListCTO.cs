using Hearth.Foundation.Enums;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Utility;
using Hearth.Services.Utility.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.CTOs.Finance.Transaction
{
    public class TransactionListCTO
    {
        #region Primary
        // Keystone Objects
        public List<TransactionDTO> Transactions { get; set; }
        public TransactionDTO? SingleTransaction { get; set; } = null;
        // CTO Options
        public TransactionListCTO_O Options { get; set; } = new TransactionListCTO_O();
        #endregion
        #region Calculated (All)
        public decimal Sum_Outgoing_C
        {
            get
            {
                var result = 0.00m;

                decimal totalOutgoing = Transactions!
                    .Where(t => t.IsIncome_C == false)
                    .Sum(t => t.Amount);

                result = Math.Abs(totalOutgoing);

                return result;
            }
        }
        public decimal Sum_Incoming_C
        {
            get
            {
                var result = 0.00m;

                decimal totalIncoming = Transactions!
                    .Where(t => t.IsIncome_C == true)
                    .Sum(t => t.Amount);

                result = Math.Abs(totalIncoming);

                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Average_OutgoingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                // By First Transaction. - Old Calculation
                // This is how it used to work, and I might still bring this back for a different _C variable at some point.

                // DateTime earliestTransactionDate = Transactions!
                //    .Select(t => DateTime.TryParse(t.Date, out var date) ? date : DateTime.MaxValue)
                //    .DefaultIfEmpty(DateTime.MaxValue)
                //    .Min();

                // By Year - New calculation
                DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, firstDayOfYear, lastDayOfYear);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Outgoing_C);

                return result;
            }
        }
        public decimal Average_IncomingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, firstDayOfYear, lastDayOfYear);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Incoming_C);

                return result;
            }
        }
        // Category Calculations
        public Dictionary<int, decimal> Sum_OutgoingCategories_C
        {
            get
            {
                return Transactions!
                    .Where(t => t.IsIncome_C == false && t.CategoryId != null)
                    .GroupBy(t => t.CategoryId!.Value)
                    .ToDictionary(
                        group => group.Key,
                        group => Math.Abs(group.Sum(t => t.Amount))
                    );
            }
        }
        public Dictionary<int, decimal> Sum_IncomingCategories_C
        {
            get
            {
                return Transactions!
                    .Where(t => t.IsIncome_C && t.CategoryId != null)
                    .GroupBy(t => t.CategoryId!.Value)
                    .ToDictionary(
                        group => group.Key,
                        group => Math.Abs(group.Sum(t => t.Amount))
                    );
            }
        }

        public List<CategoryAmountPoint> PieChart_Outgoing_CategoryAmount_C
        {
            get
            {
                var result = Transactions
                    .Where(t => !t.IsIncome_C)
                    .GroupBy(t => t.CategoryId)
                    .Select(g => new CategoryAmountPoint(
                        g.First().Category?.Name ?? "Uncategorized",
                        Math.Abs(g.Sum(t => t.Amount)),
                        g.First().Category?.HexColor))
                    .OrderByDescending(p => p.Amount)
                    .ToList();

                return result;
            }
        }
        #endregion
        #region Calculated (Primary (Current) Year)
        public decimal Sum_Primary_Outgoing_C
        {
            get
            {
                var result = 0.00m;

                decimal totalOutgoing = Transactions!
                    .Where(t => t.IsIncome_C == false && t.Date_C.Year == Options.PrimaryYear)
                    .Sum(t => t.Amount);

                result = Math.Abs(totalOutgoing);

                return result;
            }
        }
        public decimal Sum_Primary_Incoming_C
        {
            get
            {
                var result = 0.00m;

                decimal totalIncoming = Transactions!
                    .Where(t => t.IsIncome_C == true && t.Date_C.Year == Options.PrimaryYear)
                    .Sum(t => t.Amount);

                result = Math.Abs(totalIncoming);

                return result;
            }
        }
        public decimal Average_Primary_OutgoingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, firstDayOfYear, lastDayOfYear);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Primary_Outgoing_C);

                return result;
            }
        }
        public decimal Average_Primary_IncomingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, firstDayOfYear, lastDayOfYear);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Primary_Incoming_C);

                return result;
            }
        }
        public bool Is_Primary_Outgoing_LessThan_Secondary_Outgoing_C
        {
            get
            {
                var result = false;

                if (Sum_Primary_Outgoing_C <= Sum_Secondary_Outgoing_C) result = true;

                return result;
            }
        }
        public bool Is_Primary_Incoming_GreaterThan_Secondary_Incoming_C
        {
            get
            {
                var result = false;

                if (Sum_Primary_Incoming_C >= Sum_Secondary_Incoming_C) result = true;

                return result;
            }
        }

        public List<CategoryAmountPoint> PieChart_Primary_Outgoing_CategoryAmount_C
        {
            get
            {
                var result = Transactions
                    .Where(t => !t.IsIncome_C && t.Date_C.Year == Options.PrimaryYear)
                    .GroupBy(t => t.CategoryId)
                    .Select(g => new CategoryAmountPoint(
                        g.First().Category?.Name ?? "Uncategorized",
                        Math.Abs(g.Sum(t => t.Amount)),
                        g.First().Category?.HexColor))
                    .OrderByDescending(p => p.Amount)
                    .ToList();

                return result;
            }
        }
        #endregion
        #region Calculated (Secondary (Comparison) Year)
        public decimal Sum_Secondary_Outgoing_C
        {
            get
            {
                var result = 0.00m;

                decimal totalOutgoing = Transactions!
                    .Where(t => t.IsIncome_C == false && t.Date_C.Year == Options.SecondaryYear)
                    .Sum(t => t.Amount);

                result = Math.Abs(totalOutgoing);

                return result;
            }
        }
        public decimal Sum_Secondary_Incoming_C
        {
            get
            {
                var result = 0.00m;

                decimal totalIncoming = Transactions!
                    .Where(t => t.IsIncome_C == true && t.Date_C.Year == Options.SecondaryYear)
                    .Sum(t => t.Amount);

                result = Math.Abs(totalIncoming);

                return result;
            }
        }

        public decimal Average_Secondary_OutgoingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, firstDayOfYear, lastDayOfYear);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Secondary_Outgoing_C);

                return result;
            }
        }
        public decimal Average_Secondary_IncomingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, firstDayOfYear, lastDayOfYear);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Secondary_Incoming_C);

                return result;
            }
        }

        public List<CategoryAmountPoint> PieChart_Secondary_Outgoing_CategoryAmount_C
        {
            get
            {
                var result = Transactions
                    .Where(t => !t.IsIncome_C && t.Date_C.Year == Options.SecondaryYear)
                    .GroupBy(t => t.CategoryId)
                    .Select(g => new CategoryAmountPoint(
                        g.First().Category?.Name ?? "Uncategorized",
                        Math.Abs(g.Sum(t => t.Amount)),
                        g.First().Category?.HexColor))
                    .OrderByDescending(p => p.Amount)
                    .ToList();

                return result;
            }
        }
        #endregion
        #region Calculated (Single)
        public decimal Single_OutgoingInRelationToList_Percent_C
        {
            get
            {
                var result = 0.00m;

                if (SingleTransaction == null) return 0.00m;
                if (SingleTransaction.IsIncome_C) return 0.00m;

                decimal outgoingAmount = Math.Abs(SingleTransaction.Amount);

                result = (outgoingAmount / Sum_Outgoing_C);

                return result;
            }
        }
        public decimal Single_IncomingInRelationToList_Percent_C
        {
            get
            {
                var result = 0.00m;

                if (SingleTransaction == null) return 0.00m;
                if (SingleTransaction.IsIncome_C == false) return 0.00m;

                decimal incomeAmount = Math.Abs(SingleTransaction.Amount);

                result = (incomeAmount / Sum_Incoming_C);

                return result;
            }
        }
        public decimal Single_OutgoingInRelationToCategory_Percent_C
        {
            get
            {
                var result = 0.00m;

                if (SingleTransaction == null) return 0.00m;
                if (SingleTransaction.IsIncome_C) return 0.00m;

                decimal outgoingAmount = Math.Abs(SingleTransaction.Amount);
                decimal sumOfCategory = 0m;

                if (SingleTransaction.CategoryId == null)
                {
                    sumOfCategory = Transactions.Where(t => t.CategoryId == null).Sum(t => t.Amount);
                }
                else
                {

                    if (SingleTransaction?.CategoryId is int categoryId)
                    {
                        Sum_OutgoingCategories_C.TryGetValue(categoryId, out sumOfCategory);
                    }

                }

                if (sumOfCategory == 0) return 1;

                result = (outgoingAmount / sumOfCategory);

                return result;
            }
        }

        public decimal Single_IncomingInRelationToCategory_Percent_C
        {
            get
            {
                var result = 0.00m;

                if (SingleTransaction == null) return 0.00m;
                if (SingleTransaction.IsIncome_C == false) return 0.00m;

                decimal incomingAmount = Math.Abs(SingleTransaction.Amount);
                decimal sumOfCategory = 0m;

                if (SingleTransaction.CategoryId == null)
                {
                    sumOfCategory = Transactions
                        .Where(t => t.CategoryId == null && t.IsIncome_C)
                        .Sum(t => t.Amount);
                }
                else
                {

                    if (SingleTransaction?.CategoryId is int categoryId)
                    {
                        Sum_IncomingCategories_C.TryGetValue(categoryId, out sumOfCategory);
                    }

                }

                result = (incomingAmount / sumOfCategory);

                return result;
            }
        }
        #endregion
        #region Formated
        public TransactionListCTO_F _formated;
        public TransactionListCTO_F Formated => _formated ??= new TransactionListCTO_F(this);
        #endregion
    }

    public class TransactionListCTO_F
    {
        private readonly TransactionListCTO _calculated;

        public TransactionListCTO_F(TransactionListCTO calculated)
        {
            _calculated = calculated;
        }
        // TODO there needs to be a better way to determine which currency code to be used, rather than just which kind appears first in the list
        private string CurrencyCode_C => _calculated.Transactions.FirstOrDefault()?.CurrencyCode_S ?? "USD";
        // All
        public string Sum_Outgoing_Num_S =>
            CurrencySymbolHelper
            .ConvertDecimalToCurrency(
                _calculated.Sum_Outgoing_C,
                CurrencyCode_C
            );

        public string Sum_Incoming_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Sum_Incoming_C,
                CurrencyCode_C
            );

        public string Average_OutgoingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_OutgoingPerPeriod_Num_C,
                CurrencyCode_C
            );

        public string Average_IncomingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_IncomingPerPeriod_Num_C,
                CurrencyCode_C
            );

        // Primary
        public string Sum_Primary_Outgoing_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Sum_Primary_Outgoing_C,
                CurrencyCode_C
            );

        public string Sum_Primary_Incoming_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Sum_Primary_Incoming_C,
                CurrencyCode_C
            );

        public string Average_Primary_OutgoingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_Primary_OutgoingPerPeriod_Num_C,
                CurrencyCode_C
            );

        public string Average_Primary_IncomingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_Primary_IncomingPerPeriod_Num_C,
                CurrencyCode_C
            );
        // Secondary
        public string Sum_Secondary_Outgoing_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Sum_Secondary_Outgoing_C,
                CurrencyCode_C
            );

        public string Sum_Secondary_Incoming_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Sum_Secondary_Incoming_C,
                CurrencyCode_C
            );

        public string Average_Secondary_OutgoingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_Secondary_OutgoingPerPeriod_Num_C,
                CurrencyCode_C
            );

        public string Average_Secondary_IncomingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_Secondary_IncomingPerPeriod_Num_C,
                CurrencyCode_C
            );
        // Single
        public string Single_IncomingInRelationToList_Percent_S =>
        _calculated.Single_IncomingInRelationToList_Percent_C.ToString("P2");

        public string Single_OutgoingInRelationToList_Percent_S =>
        _calculated.Single_OutgoingInRelationToList_Percent_C.ToString("P2");

        public string Single_OutgoingInRelationToCategory_Percent_S =>
            _calculated.Single_OutgoingInRelationToCategory_Percent_C.ToString("P2");

        public string Single_IncomingInRelationToCategory_Percent_S =>
            _calculated.Single_IncomingInRelationToCategory_Percent_C.ToString("P2");

    }

    public class TransactionListCTO_O
    {
        public bool IncludeTransfers { get; set; } = true;
        public E_CALENDAR_FREQUENCY Frequency { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
        /// <summary>
        /// Usually represents the Current year, but can be changed for specific reporting needs
        /// </summary>
        public int PrimaryYear { get; set; } = (DateTime.Today.Year);
        /// <summary>
        /// Usually represents the Previous year, but can be changed for specific reporting needs
        /// </summary>
        public int SecondaryYear { get; set; } = (DateTime.Today.Year - 1);
        /// <summary>
        /// Usually represents the Current month, but can be changed for specific reporting needs
        /// </summary>
        public int PrimaryMonth { get; set; } = DateTime.Today.Month;
        /// <summary>
        /// Usually represents the Previous month, but can be changed for specific reporting needs
        /// </summary>
        public int SecondaryMonth { get; set; } = DateTime.Today.Month - 1;



        /* 
         * The ideas below are very advanced and may be reviseted at a future date, but for now are out of the scope for this project.
         * 
        /// <summary>
        /// This is usually just the current frequency period, but can be increased to select a specific period.
        /// 0 = current unfinished period. It is Inclusive. This variable is also always dynamic, and cannot be locked to a specific time frame, like PrimaryMonth and PrimaryYear can.
        /// </summary>
        public int PrimaryPeriod { get; set; } = 0;
        /// <summary>
        /// This represents how many frequency periods back the PreviousSecondaryPeriod calculations will compare the current period against.
        /// 1 = previous complete period. 2,3,4,etc just go further back periods. This variable is also always dynamic, and cannot be locked to a specific time frame, like PrimaryMonth and PrimaryYear can.
        /// </summary>
        public int SecondaryPeriod { get; set; } = 1;

        */
    }
}
