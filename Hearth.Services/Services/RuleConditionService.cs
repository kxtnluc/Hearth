using Hearth.Core.Data;
using Hearth.Core.Models;
using Hearth.Foundation.Enums;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs;
using Hearth.Services.Interfaces;
using Hearth.Services.Mapping;
using Hearth.Services.Utility.Finance;
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
        public async Task<bool> Run<T>(RuleConditionDTO condition, T item)
        {
            bool result = true;

            if (condition == null) return false;

            var numericOp = EnumDisplayHelper.FromDisplayString<E_OPERATOR>(condition.Condition);
            var stringOp = EnumDisplayHelper.FromDisplayString<E_STRING_OPERATOR>(condition.Condition);

            // condition.Condition didn't match any known operator at all
            if (numericOp is null && stringOp is null)
                return false;

            var property = typeof(T).GetProperty(condition.Field);
            if (property == null) return false;

            object? rawValue = property.GetValue(item);
            string value = rawValue?.ToString() ?? string.Empty;

            result = numericOp != null
                ? EvaluateNumeric(numericOp.Value, value, condition.Match)
                : EvaluateString(stringOp!.Value, value, condition.Match);

            return result;
        }
        #endregion
        #region Private Functions
        private static bool EvaluateNumeric(E_OPERATOR op, string value, string match)
        {
            // Trys to parse the value as a float, if it fails return false
            if (!float.TryParse(value, out var valueNum)) return false;

            if (op == E_OPERATOR.Between)
            {
                var matchRange = match.Split('-');
                if (matchRange.Length != 2) return false;
                if (!float.TryParse(matchRange[0], out var min)) return false;
                if (!float.TryParse(matchRange[1], out var max)) return false;

                return valueNum >= min && valueNum <= max;
            }

            if (!float.TryParse(match, out var matchNum)) return false;

            return op switch
            {
                E_OPERATOR.Equals => valueNum == matchNum,
                E_OPERATOR.GreaterThan => valueNum > matchNum,
                E_OPERATOR.LessThan => valueNum < matchNum,
                E_OPERATOR.GreaterThanOrEqualTo => valueNum >= matchNum,
                E_OPERATOR.LessThanOrEqualTo => valueNum <= matchNum,
                E_OPERATOR.NotEquals => valueNum != matchNum,
                _ => false,
            };
        }

        private static bool EvaluateString(E_STRING_OPERATOR op, string value, string match)
        {
            return op switch
            {
                E_STRING_OPERATOR.Exact => string.Equals(value, match, StringComparison.OrdinalIgnoreCase),
                E_STRING_OPERATOR.StartsWith => value.StartsWith(match, StringComparison.OrdinalIgnoreCase),
                E_STRING_OPERATOR.EndsWith => value.EndsWith(match, StringComparison.OrdinalIgnoreCase),
                E_STRING_OPERATOR.Contains => value.Contains(match, StringComparison.OrdinalIgnoreCase),
                E_STRING_OPERATOR.IsNot => !string.Equals(value, match, StringComparison.OrdinalIgnoreCase),
                _ => false,
            };
        }
        #endregion
    }
}