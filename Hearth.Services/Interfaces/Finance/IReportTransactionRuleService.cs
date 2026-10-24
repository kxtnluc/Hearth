using Hearth.Services.DTOs.Finance.Report;
using Hearth.Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IReportTransactionRuleService : ISqliteTableService<ReportTransactionRuleDTO, SqliteTableFilter>
    {
        Task<List<ReportTransactionRuleDTO>> GetAllByReportId(int reportId);
    }
}
