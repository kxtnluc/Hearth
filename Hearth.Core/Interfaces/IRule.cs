using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Interfaces
{
    public interface IRule : ISqliteTable
    {
        /// <summary>
        /// The name of the rule
        /// </summary>
        /// <example>Assign Food to "Target"</example>
        string Name { get; set; }
        /// <summary>
        /// Tells if the rule is to be followed or ignored
        /// </summary>
        /// <example>true</example>
        bool Active { get; set; }
        /// <summary>
        /// If this rule is run along with other rules, this shows its priority level (1 being highest)
        /// </summary>
        /// <example>1</example>
        int Priority { get; set;  }
    }
}
