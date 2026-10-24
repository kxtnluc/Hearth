using Hearth.Services.CTOs.Finance.Report;
using Hearth.Services.DTOs.Finance.Report;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IReportService : ISqliteTableService<ReportDTO, SqliteTableFilter>
    {
        Task<List<ReportDTO>> GetAllWithAll();
        Task<ReportDTO> GetByIdWithAll(int reportId);
        Task<ReportCTO> GenerateReportCTO(int reportId, ReportCTO_O? options = null);
        /// <summary>
        /// Creates a blank and/or template report and returns its ID, so that it can be edited in the Report Builder.
        /// </summary>
        /// <returns></returns>
        Task<int> CreateBuilderDraft();
        /// <summary>
        /// This deletes the report itself, along with all the rules and rules conditions assosiated.
        /// </summary>
        Task DeleteWithAll(int reportId);
    }
}
