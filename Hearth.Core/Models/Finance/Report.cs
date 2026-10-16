using Hearth.Foundation.Enums.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    public class Report
    {
        #region Generic Props
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        #endregion

        #region Model Foundational Props
        /// <summary>
        /// Every rule attached to this report. EF materializes each row as its real subtype.
        /// </summary>
        public List<ReportRule> Rules { get; set; } = new();

        [NotMapped]
        public IEnumerable<ReportTransactionRule> TransactionRules => Rules.OfType<ReportTransactionRule>();

        [NotMapped]
        public IEnumerable<ReportAccountRule> AccountRules => Rules.OfType<ReportAccountRule>();

        [NotMapped]
        public IEnumerable<ReportUserRule> UserRules => Rules.OfType<ReportUserRule>();
        /// <summary>
        /// Tells the report weather to use the HearthSettings Fiscal Year instead of just the calandar year.
        /// </summary>
        public bool IsByFiscalYear { get; set; } = false;
        /// <summary>
        /// This overwtites the default Fiscal Start and End in HearthSettings for this specific report
        /// </summary>
        public int? CustomFiscalStart { get; set; } = null;
        /// <summary>
        /// This overwtites the default Fiscal Start and End in HearthSettings for this specific report
        /// </summary>
        public int? CustomFiscalEnd { get; set; } = null;
        public bool LockPrimaryYear { get; set; } = false;
        public bool LockSecondaryYear { get; set; } = false;
        public bool HidePeriodRates { get; set; } = false;
        public bool HideCategoryGraphsRates { get; set; } = false;
        public bool HideAllTime { get; set; } = false;
        public bool HideBudget { get; set; } = true;
        #endregion
    }
}
