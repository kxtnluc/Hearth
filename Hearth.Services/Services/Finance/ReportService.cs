using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.CTOs.Finance.Report;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Report;
using Hearth.Services.Filters;
using Hearth.Services.Filters.Finance;
using Hearth.Services.Interfaces;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services.Finance
{
    public class ReportService : ASqliteTableService<Report, ReportDTO, SqliteTableFilter>, IReportService
    {
        private readonly ITransactionService _transactionService;
        private readonly IReportTransactionRuleService _reportTranCatRuleService;
        private readonly IRuleConditionService _ruleConditionService;
        public ReportService(
            HearthDbContext context,
            ITransactionService transactionService,
            IReportTransactionRuleService reportTranCatRuleService,
            IRuleConditionService ruleConditionService
        )
        : base(context)
        {
            _transactionService = transactionService;
            _reportTranCatRuleService = reportTranCatRuleService;
            _ruleConditionService = ruleConditionService;
        }
        #region Abstract Class Setup
        protected override DbSet<Report> DbSet => _context.Reports;
        protected override ReportDTO ToDto(Report entity) => entity.ToDto();
        protected override Report ToEntity(ReportDTO dto) => dto.ToEntity();
        protected override List<ReportDTO> ToDtoList(List<Report> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(ReportDTO dto, Report entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(ReportDTO payload)
        {
            // To implement, simply fill out this function
            throw new NotImplementedException();
        }
        #endregion

        #region Filter
        public override List<ReportDTO> Filter(List<ReportDTO> Reports, SqliteTableFilter filter)
        {
            // To implement, create a unique Filter object, replace it in this service, and fill out this function
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public async Task<List<ReportDTO>> GetAllWithAll()
        {
            var reports = await _context.Reports
                .AsNoTracking()
                .Include(r => r.Rules)
                    .ThenInclude(rule => rule.RuleConditions)
                    .ToListAsync();

            return ToDtoList(reports);
        }

        public async Task<ReportDTO> GetByIdWithAll(int reportId)
        {
            var report = await _context.Reports
                .AsNoTracking()
                .Include(r => r.Rules)
                    .ThenInclude(rule => rule.RuleConditions)
                .SingleOrDefaultAsync(r => r.Id == reportId)
                ?? throw new HearthRecordNotFoundException(reportId.ToString());

            return ToDto(report);
        }

        public async Task<int> CreateBuilderDraft()
        {
            ReportDTO draftReport = new ReportDTO()
            {
                Name = "New Draft Report",
            };

            var createdDraft = await this.Create(draftReport);
            return createdDraft.Id;
        }

        public async Task DeleteWithAll(int reportId)
        {
            // - Fetching Ids -
            var allRulesOnReport = await _reportTranCatRuleService.GetAllByReportId(reportId);

            int[] ruleConIds = allRulesOnReport
                .SelectMany(rule => rule.RuleConditions)
                .Select(condition => condition.Id)
                .ToArray();

            int[] ruleIds = allRulesOnReport
                .Select(r => r.Id)
                .ToArray();

            // - Deleting Rule Conditions -
            if(ruleConIds is not null) await _ruleConditionService.DeleteRange(ruleConIds, false);
            // - Deleting Rules -
            if(ruleIds is not null) await _reportTranCatRuleService.DeleteRange(ruleIds, false);
            // - Deleting Report -
            await this.Delete(reportId, true);

            return;

        }

        #endregion
        #region CTO Generation
        public async Task<ReportCTO> GenerateReportCTO(int reportId, ReportCTO_O? options = null)
        {
            var report = await GetByIdWithAll(reportId);

            var rules = report.TransactionRules.Where(r => r.Active).ToList();
            var allTransactions = await _transactionService.GetAllWithCategory();

            // No active rules: the report covers everything.
            // Otherwise a transaction belongs if it passes every condition of ANY active rule.
            var matched = rules.Count == 0
                ? allTransactions
                : allTransactions
                    .Where(t => rules.Any(rule => RuleHelper.PassesAll(rule.RuleConditions, t)))
                    .ToList();

            return new ReportCTO
            {
                Report = report,
                Transactions = matched,
                Options = options ?? new ReportCTO_O()
            };
        }
        #endregion
    }
}