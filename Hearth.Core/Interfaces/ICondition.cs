using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Interfaces
{
    public interface ICondition : ISqliteTable
    {
        /// <summary>
        /// The rule table that this condition is referencing
        /// </summary>
        /// <example>"equals"</example>
        string RuleTable { get; set; }
        /// <summary>
        /// The specific rule that this condition is assigned to
        /// </summary>
        /// <example>"equals"</example>
        int RuleId { get; set; }
        /// <summary>
        /// The condition of the rule
        /// </summary>
        /// <example>"equals"</example>
        string Condition { get; set; }
        /// <summary>
        /// The field that the condition is checking
        /// </summary>
        /// <example>"Merchant_Name"</example>
        string Field { get; set; }
        /// <summary>
        /// The value/match of what the field needs to compare against to see if the condition passes or fails
        /// </summary>
        /// <example>"Target"</example>
        string Match { get; set; }
    }
}
