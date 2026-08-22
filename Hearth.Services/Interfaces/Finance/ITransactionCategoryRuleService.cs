using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.DTOs.Finance.TransactionCategory;
using Hearth.Services.Filters;

namespace Hearth.Services.Interfaces.Finance
{
    public interface ITransactionCategoryRuleService : ISqliteTableService<TransactionCategoryRuleDTO, SqliteTableFilter>
    {
        /// <summary>
        /// Gets a specific transaction category rule by its ID, including its associated rule conditions.
        /// </summary>
        /// <param name="transactionCategoryRuleId"></param>
        /// <returns></returns>
        Task<TransactionCategoryRuleDTO?> GetByIdWithRuleConditions(int transactionCategoryRuleId);
        /// <summary>
        /// Runs a specific rule set against a list of transactions and returns the number of transactions that passed the rule conditions.
        /// </summary>
        /// <param name="transactionCategoryRuleId"></param>
        /// <param name="transactions"></param>
        /// <returns></returns>
        Task<int> RunRuleSet(int transactionCategoryRuleId, List<TransactionDTO> transactions);
        Task RunAllRuleSets(List<TransactionDTO> transactions);
    }
}
