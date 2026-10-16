using Hearth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs.Finance.Report
{
    public abstract class ReportRuleDTO : RuleDTO
    {
        public int ReportId { get; set; }
    }
    public class ReportTransactionRuleDTO : ReportRuleDTO { }
    public class ReportAccountRuleDTO : ReportRuleDTO { }
    public class ReportUserRuleDTO : ReportRuleDTO { }
}
