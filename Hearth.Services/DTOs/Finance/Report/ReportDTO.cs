using Hearth.Core.Models.Finance;
using Hearth.Services.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hearth.Services.DTOs.Finance.Report
{
    public class ReportDTO : IDTO
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
        public List<ReportRuleDTO> Rules { get; set; } = new();

        [NotMapped]
        public IEnumerable<ReportTransactionRuleDTO> TransactionRules => Rules.OfType<ReportTransactionRuleDTO>();

        [NotMapped]
        public IEnumerable<ReportAccountRuleDTO> AccountRules => Rules.OfType<ReportAccountRuleDTO>();

        [NotMapped]
        public IEnumerable<ReportUserRuleDTO> UserRules => Rules.OfType<ReportUserRuleDTO>();
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
        public bool HideCategoryGraphs { get; set; } = false;
        public bool HideAllTimeSection { get; set; } = false;
        public bool HideBudget { get; set; } = true;
        #endregion
    }
}
