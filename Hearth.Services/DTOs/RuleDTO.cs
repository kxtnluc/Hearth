using Hearth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs
{
    public abstract class RuleDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Active { get; set; } = true;
        public int Priority { get; set; }
        public List<RuleConditionDTO> RuleConditions { get; set; } = new();
    }
}
