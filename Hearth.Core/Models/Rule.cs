using Hearth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models
{
    // Hearth.Core/Models/Rule.cs — the shared base table
    public abstract class Rule : IRule
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Active { get; set; } = true;
        public int Priority { get; set; }

        public List<RuleCondition> RuleConditions { get; set; } = new();
    }
}
