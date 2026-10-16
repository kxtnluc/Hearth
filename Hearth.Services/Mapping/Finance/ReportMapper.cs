using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Report;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Mapping.Finance
{
    [Mapper]
    internal static partial class ReportMapper
    {
        [MapDerivedType<ReportTransactionRule, ReportTransactionRuleDTO>]
        [MapDerivedType<ReportAccountRule, ReportAccountRuleDTO>]
        [MapDerivedType<ReportUserRule, ReportUserRuleDTO>]
        public static partial ReportRuleDTO ToDto(this ReportRule entity);

        public static partial ReportDTO ToDto(this Report entity);
        /// <summary>
        /// Turns a DTO back into its Model (entity)
        /// </summary>
        [MapperIgnoreSource(nameof(ReportDTO.Rules))]
        [MapperIgnoreTarget(nameof(Report.Rules))]
        public static partial Report ToEntity(this ReportDTO dto);
        /// <summary>
        /// Turns a Model List into its DTO List counterpart
        /// </summary>
        public static partial List<ReportDTO> ToDtoList(this List<Report> entities);

        [MapperIgnoreSource(nameof(ReportDTO.Rules))]
        [MapperIgnoreTarget(nameof(Report.Rules))]
        public static partial void ApplyUpdate(this ReportDTO dto, Report entity);
    }
}
