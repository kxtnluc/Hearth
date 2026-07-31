using Hearth.Core.Interfaces;
using Hearth.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models
{
    public class RuleCondition : ICondition
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
