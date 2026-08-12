using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.DTOs.Finance.TransactionCategory;
using Hearth.Services.Interfaces;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Services.Finance
{
    public class TransactionCategoryRuleService : ASqliteTableService<TransactionCategoryRule, TransactionCategoryRuleDTO>, ITransactionCategoryRuleService
    {
        private readonly IRuleConditionService _ruleConditionService;
        private readonly ITransactionService _transactionService;

        public TransactionCategoryRuleService(
            HearthDbContext context, 
            IRuleConditionService ruleConditionService, 
            ITransactionService transactionService
        )
        : base(context)
        {
            _ruleConditionService = ruleConditionService;
            _transactionService = transactionService;
        }

        #region Abstract Class Setup
        protected override DbSet<TransactionCategoryRule> DbSet => _context.TransactionCategoryRules;
        protected override TransactionCategoryRuleDTO ToDto(TransactionCategoryRule entity) => entity.ToDto();
        protected override TransactionCategoryRule ToEntity(TransactionCategoryRuleDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(TransactionCategoryRuleDTO dto, TransactionCategoryRule entity) => dto.ApplyUpdate(entity);
        #endregion
        #region Model Specific Functions
        public async Task<TransactionCategoryRuleDTO> GetByIdWithRuleConditions(int transactionCategoryRuleId)
        {
            // THIS cannot just be a simple include. It has to map via table name and row id, so this needs to be more complex ):<
            var transactionCategoryRule = await _context.TransactionCategoryRules
                .Include(tcr => tcr.RuleConditions)
                .FirstOrDefaultAsync(tcr => tcr.Id == transactionCategoryRuleId);

            return transactionCategoryRule?.ToDto();
        }

        public async Task<int> RunRuleSet(int transactionCategoryRuleId, List<TransactionDTO> transactions)
        {
            var transactionCategoryRule = await GetByIdWithRuleConditions(transactionCategoryRuleId);

            if (transactionCategoryRule == null || !transactionCategoryRule.Active) return 0;

            var matchedTransactions = new List<TransactionDTO>();

            foreach (var t in transactions)
            {
                var evaluations = await Task.WhenAll(
                    transactionCategoryRule.RuleConditions.Select(rc => _ruleConditionService.Run(rc, t))
                );

                bool allConditionsPassed = evaluations.All(result => result);

                if (allConditionsPassed)
                {
                    t.CategoryId = transactionCategoryRule.TransactionCategoryId;
                    matchedTransactions.Add(t);
                }
            }

            await _transactionService.UpdateRange(matchedTransactions);

            return matchedTransactions.Count;
        }
        public async Task RunAllRuleSets(List<TransactionDTO> transactions)
        {

            return;
        }
        #endregion

    }
}
