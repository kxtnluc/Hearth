using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.CTOs.Finance.Report;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Report;
using Hearth.Services.Filters;
using Hearth.Services.Filters.Finance;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services.Finance
{
    public class ReportTransactionRuleService : ASqliteTableService<ReportTransactionRule, ReportTransactionRuleDTO, SqliteTableFilter>, IReportTransactionRuleService
    {
        public ReportTransactionRuleService(HearthDbContext context) : base(context) { }

        #region Abstract Class Setup
        protected override DbSet<ReportTransactionRule> DbSet => _context.ReportTransactionRules;
        protected override ReportTransactionRuleDTO ToDto(ReportTransactionRule entity) => entity.ToDto();
        protected override ReportTransactionRule ToEntity(ReportTransactionRuleDTO dto) => dto.ToEntity();
        protected override List<ReportTransactionRuleDTO> ToDtoList(List<ReportTransactionRule> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(ReportTransactionRuleDTO dto, ReportTransactionRule entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(ReportTransactionRuleDTO payload)
        {
            if (string.IsNullOrWhiteSpace(payload.Name))
                throw new HearthInvalidPayloadException("Rule name is required.");
            if (payload.ReportId <= 0)
                throw new HearthInvalidPayloadException("Rule must belong to a report.");
        }
        #endregion

        #region Filter
        public override List<ReportTransactionRuleDTO> Filter(List<ReportTransactionRuleDTO> rules, SqliteTableFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions


        #endregion
    }
}