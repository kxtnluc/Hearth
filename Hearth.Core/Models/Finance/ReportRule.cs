using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    public abstract class ReportRule : Rule
    {
        public int ReportId { get; set; }
    }
    public class ReportTransactionRule : ReportRule { }
    public class ReportAccountRule : ReportRule { }
    public class ReportUserRule : ReportRule { }
}
