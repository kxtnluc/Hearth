using Hearth.Foundation.Enums;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Utility;
using Hearth.Services.Utility.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.CTOs.Finance.Account
{
    public class AccountCTO
    {
        #region Primary
        // Keystone Object
        public AccountDTO Account { get; set; }
        // CTO Options
        public bool IncludeTransfers { get; set; } = false;
        public E_CALENDAR_FREQUENCY Frequency { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
        #endregion
        #region _Calcualted
        public decimal Average_OutgoingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime earliestTransactionDate = Account.Transactions!
                    .Select(t => DateTime.TryParse(t.Date, out var date) ? date : DateTime.MaxValue)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

                decimal totalOutgoing = Account.Transactions!
                    .Where(t => t.IsIncome_C == false)
                    .Sum(t => t.Amount);

                totalOutgoing = Math.Abs(totalOutgoing);

                var periodCount = DateTimeHelper.GetPeriodCount(Frequency, earliestTransactionDate, DateTime.Now);

                result = FinanceCalculator.AveragePerPeriod(periodCount, totalOutgoing);

                return result;
            }
        }
        public decimal Average_IncomingPerPeriod_Num_C
        {
            get
            {
                var result = 0.00m;

                DateTime earliestTransactionDate = Account.Transactions!
                    .Select(t => DateTime.TryParse(t.Date, out var date) ? date : DateTime.MaxValue)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

                decimal totalIncoming = Account.Transactions!
                    .Where(t => t.IsIncome_C == true)
                    .Sum(t => t.Amount);

                totalIncoming = Math.Abs(totalIncoming);

                var periodCount = DateTimeHelper.GetPeriodCount(Frequency, earliestTransactionDate, DateTime.Now);

                result = FinanceCalculator.AveragePerPeriod(periodCount, totalIncoming);

                return result;
            }
        }
        public decimal Average_IncomingPerPeriod_Percent_C
        {
            get
            {
                var result = 0.00m;

                var averagePerPeriod = Average_IncomingPerPeriod_Num_C;

                var currentBalance = Account.Balances?.Current;

                if (currentBalance == null || currentBalance == 0) return 0.00m;

                result = (averagePerPeriod / currentBalance.Value);

                return result;
            }
        }
        public decimal Average_OutgoingPerPeriod_Percent_C
        {
            get
            {
                var result = 0.00m;

                var averagePerPeriod = Average_OutgoingPerPeriod_Num_C;

                var currentBalance = Account.Balances?.Current;

                if (currentBalance == null || currentBalance == 0) return 0.00m;

                result = (averagePerPeriod / currentBalance.Value);

                return result;
            }
        }
        #endregion
        #region Formated
        public AccountCTO_S _formated;
        public AccountCTO_S Formated => _formated ??= new AccountCTO_S(this);
        #endregion
    }

    public class AccountCTO_Options
    {
        public bool IncludeTransfers { get; set; } = true;
        public E_CALENDAR_FREQUENCY Frequency { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
    }

    public class AccountCTO_S
    {
        private readonly AccountCTO _calculated;

        public AccountCTO_S(AccountCTO calculated)
        {
            _calculated = calculated;
        }

        public string Average_OutgoingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_OutgoingPerPeriod_Num_C,
                _calculated.Account.Balances.CurrencyCode_S
            );

        public string Average_IncomingPerPeriod_Num_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(
                _calculated.Average_IncomingPerPeriod_Num_C,
                _calculated.Account.Balances.CurrencyCode_S
            );

        public string Average_OutgoingPerPeriod_Percent_S =>
            _calculated.Average_OutgoingPerPeriod_Percent_C.ToString("P1");

        public string Average_IncomingPerPeriod_Percent_S =>
            _calculated.Average_IncomingPerPeriod_Percent_C.ToString("P1");

    }
}
