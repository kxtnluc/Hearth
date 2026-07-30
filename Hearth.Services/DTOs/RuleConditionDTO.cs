using Hearth.Foundation.Enums;
using Hearth.Services.DTOs.Finance.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs
{
    public class RuleConditionDTO
    {
        #region Key Props
        public int Id { get; set; }
        public string RuleTable { get; set; } = default!;
        public int RuleId { get; set; }
        #endregion
        #region Model Props
        public string Field { get; set; } = default!;
        public string Condition { get; set; } = string.Empty;
        public string Match { get; set; } = string.Empty;
        #endregion
    }
}
