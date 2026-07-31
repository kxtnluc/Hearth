using Hearth.Core.Data;
using Hearth.Core.Models;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs;
using Hearth.Services.Interfaces;
using Hearth.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services
{
    public class RuleConditionService : ASqliteTableService<RuleCondition, RuleConditionDTO>, IRuleConditionService
    {
        public RuleConditionService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<RuleCondition> DbSet => _context.RuleConditions;
        protected override RuleConditionDTO ToDto(RuleCondition entity) => entity.ToDto();
        protected override RuleCondition ToEntity(RuleConditionDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(RuleConditionDTO dto, RuleCondition entity) => dto.ApplyUpdate(entity);
        #endregion

        #region Model Specific Functions
        public async Task<bool> Run(int conditionId, string value)
        {
            RuleConditionDTO? condition = await this.GetById(conditionId);

            // TODO
            // Needs to check what the condition.Condition is, maybe change some stuff to Enums and string conversions?
                // Then needs to check if it passes the condition test


            if (condition?.Match == value)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}