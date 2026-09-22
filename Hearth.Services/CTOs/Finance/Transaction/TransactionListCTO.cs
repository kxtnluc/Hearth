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
        #region Calculated (List)
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
        public decimal Average_OutgoingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime earliestTransactionDate = Transactions!
                    .Select(t => DateTime.TryParse(t.Date, out var date) ? date : DateTime.MaxValue)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, earliestTransactionDate, DateTime.Now);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Outgoing_C);

                return result;
            }
        }
        public decimal Average_IncomingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime earliestTransactionDate = Transactions!
                    .Select(t => DateTime.TryParse(t.Date, out var date) ? date : DateTime.MaxValue)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, earliestTransactionDate, DateTime.Now);

                result = FinanceCalculator.AveragePerPeriod(periodCount, Sum_Incoming_C);

                return result;
            }
        }
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
        public string Average_OutgoingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_OutgoingPerPeriod_Num_C,
                _calculated.Transactions[0].CurrencyCode_S
            );

        public string Average_IncomingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_IncomingPerPeriod_Num_C,
                _calculated.Transactions[0].CurrencyCode_S
            );

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
    }
}
