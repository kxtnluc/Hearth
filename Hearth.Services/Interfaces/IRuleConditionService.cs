using Hearth.Services.DTOs;
using Hearth.Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    public interface IRuleConditionService : ISqliteTableService<RuleConditionDTO, SqliteTableFilter>
    {
        Task<bool> Run<T>(RuleConditionDTO rc, T item);
    }
}
