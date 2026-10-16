using Hearth.Core.Data;
using Hearth.Core.Models;
using Hearth.Core.Models.Finance;
using Hearth.Foundation.Enums;
using Hearth.Services.Abstract;
using Hearth.Services.CTOs.Finance.Report;
using Hearth.Services.DTOs;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;
using Hearth.Services.Interfaces;
using Hearth.Services.Mapping;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services
{
    public class RuleConditionService : ASqliteTableService<RuleCondition, RuleConditionDTO, SqliteTableFilter>, IRuleConditionService
    {
        public RuleConditionService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<RuleCondition> DbSet => _context.RuleConditions;
        protected override RuleConditionDTO ToDto(RuleCondition entity) => entity.ToDto();
        protected override RuleCondition ToEntity(RuleConditionDTO dto) => dto.ToEntity();
        protected override List<RuleConditionDTO> ToDtoList(List<RuleCondition> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(RuleConditionDTO dto, RuleCondition entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(RuleConditionDTO payload)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public Task<bool> Run<T>(RuleConditionDTO condition, T item)
            => Task.FromResult(RuleHelper.Evaluate(condition, item));
        #endregion

        #region Filter
        public override List<RuleConditionDTO> Filter(List<RuleConditionDTO> banks, SqliteTableFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}