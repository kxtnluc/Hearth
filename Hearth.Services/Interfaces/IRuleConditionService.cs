using Hearth.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces
{
    public interface IRuleConditionService : ISqliteTableService<RuleConditionDTO>
    {
        Task<bool> Run<T>(RuleConditionDTO rc, T item);
    }
}
