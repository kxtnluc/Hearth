using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.DTOs.Finance.TransactionCategory;
using Hearth.Services.Filters;
using Hearth.Services.Filters.Finance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance;

public interface ITransactionCategoryService : ISqliteTableService<TransactionCategoryDTO, SqliteTableFilter>
{
    Task<TransactionCategoryDTO?> GetByIdWithRules(int id);
    Task<Dictionary<int, string>> GetAllAsDictionary();
}

