using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Report;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Mapping.Finance
{
    [Mapper]
    internal static partial class ReportTransactionRuleMapper
    {
        /// <summary>
        /// Turns a Model (entity) into its DTO
        /// </summary>
        public static partial ReportTransactionRuleDTO ToDto(this ReportTransactionRule entity);

        /// <summary>
        /// Turns a DTO back into its Model (entity). Conditions are managed by RuleConditionService.
        /// </summary>
        [MapperIgnoreSource(nameof(ReportTransactionRuleDTO.RuleConditions))]
        [MapperIgnoreTarget(nameof(ReportTransactionRule.RuleConditions))]
        public static partial ReportTransactionRule ToEntity(this ReportTransactionRuleDTO dto);

        /// <summary>
        /// Turns a Model List into its DTO List counterpart
        /// </summary>
        public static partial List<ReportTransactionRuleDTO> ToDtoList(this List<ReportTransactionRule> entities);

        /// <summary>
        /// Applies DTO values onto an existing tracked entity. Conditions are never replaced here.
        /// </summary>
        [MapperIgnoreSource(nameof(ReportTransactionRuleDTO.RuleConditions))]
        [MapperIgnoreTarget(nameof(ReportTransactionRule.RuleConditions))]
        public static partial void ApplyUpdate(this ReportTransactionRuleDTO dto, ReportTransactionRule entity);
    }
}
