using Hearth.Core.Data;
using Hearth.Foundation.Enums;
using Hearth.Services.CTOs.Finance.Transaction;
using Hearth.Services.DTOs.Finance.Report;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Utility;
using Hearth.Services.Utility.Finance;

namespace Hearth.Services.CTOs.Finance.Report
{
    public class ReportCTO
    {

        #region Primary
        // Keystone Objects
        public ReportDTO? Report { get; set; } = null;
        // CTO Options
        public ReportCTO_O Options { get; set; } = new ReportCTO_O();
        public List<TransactionDTO> Transactions { get; set; } = new();
        #endregion

        #region Nested CTO
        /// <summary>
        /// Reuses all the transaction math (sums, averages, category breakdowns)
        /// on just this report's matched transactions.
        /// </summary>
        private TransactionListCTO? _transactionStats;
        public TransactionListCTO TransactionStats => _transactionStats ??= new TransactionListCTO
        {
            Transactions = Transactions,
            Options = { 
                Frequency = Options.Frequency,
                PrimaryYear = Options.PrimaryYear,
                SecondaryYear = Options.SecondaryYear
            }
        };
        #endregion
        #region Calculated
        public int TransactionMatchCount_C => Transactions.Count;
        //public int AccountMatchCount_C => Transactions.Count;
        //public int UserMatchCount_C => Transactions.Count;


        #endregion

        #region Formated
        private ReportCTO_F? _formated;
        public ReportCTO_F Formated => _formated ??= new ReportCTO_F(this);
        #endregion
    }

    /// <summary>
    /// Formatted (_S) display strings for every calculated (_C) value on the CTO.
    /// </summary>
    public class ReportCTO_F
    {
        private readonly ReportCTO _calculated;

        public ReportCTO_F(ReportCTO calculated)
        {
            _calculated = calculated;
        }
    }

    /// <summary>
    /// Options that change how the CTO calculates.
    /// </summary>
    public class ReportCTO_O
    {
        public E_CALENDAR_FREQUENCY Frequency { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
        public int PrimaryYear { get; set; } = (DateTime.Today.Year);
        public int SecondaryYear { get; set; } = (DateTime.Today.Year - 1);
    }
}
