using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace Hearth.Services.Filters.Finance
{
    public class AccountFilter : SqliteTableFilter
    {
        public string? Name { get; set; } = null;
        public string? Official_Name { get; set; } = null;
        /// <summary>
        /// Type is a Dropdown, so is treated differently
        /// </summary>
        public string? Type
        {
            get => _type;
            set => _type = FilterHelper.NormalizeAnyOption(value);
        }
        private string? _type;
        public string? Subtype
        {
            get => _subtype;
            set => _subtype = FilterHelper.NormalizeAnyOption(value);
        }
        private string? _subtype;
        public string? Account_Id { get; set; } = null;
        public decimal? Balances_Current_Min { get; set; } = null;
        public decimal? Balances_Current_Max { get; set; } = null;
        public decimal? Balances_Available_Min { get; set; } = null;
        public decimal? Balances_Available_Max { get; set; } = null;
        public decimal? Balances_Limit_Min { get; set; } = null;
        public decimal? Balances_Limit_Max { get; set; } = null;
        public string? Iso_Currency_Code { get; set; } = null;
        public string? Bank { get; set; } = null; //idk how tf im gonna handle this one
        public static IQueryable<AccountDTO> BuildQuery(IQueryable<AccountDTO> query, AccountFilter filter)
        {
            // Exact Account Id Search
            if (filter.Account_Id != null) query = query
                    .Where(a => a.Account_Id == filter.Account_Id);

            // Soft/partial Name match
            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(a =>
                    a.Name != null &&
                    a.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));

            // Soft/partial Official_Name match
            if (!string.IsNullOrWhiteSpace(filter.Official_Name))
                query = query.Where(a =>
                    a.Official_Name != null &&
                    a.Official_Name.Contains(filter.Official_Name, StringComparison.OrdinalIgnoreCase));

            // Balances_Current is >= Min
            if (filter.Balances_Current_Min != null)
                query = query.Where(a => a.Balances!.Current >= filter.Balances_Current_Min);
            // Balances_Current is <= Max
            if (filter.Balances_Current_Max != null)
                query = query.Where(a => a.Balances!.Current <= filter.Balances_Current_Max);

            // Balances_Available is >= Min
            if (filter.Balances_Available_Min != null)
                query = query.Where(a => a.Balances!.Available >= filter.Balances_Available_Min);
            // Balances_Available is <= Max
            if (filter.Balances_Available_Max != null)
                query = query.Where(a => a.Balances!.Available <= filter.Balances_Available_Max);

            // Balances_Limit is >= Min
            if (filter.Balances_Limit_Min != null)
                query = query.Where(a => a.Balances!.Limit >= filter.Balances_Limit_Min);
            // Balances_Limit is <= Max
            if (filter.Balances_Limit_Max != null)
                query = query.Where(a => a.Balances!.Limit <= filter.Balances_Limit_Max);

            // Exact Type
            if (!string.IsNullOrWhiteSpace(filter.Type)) query = query
                    .Where(a => a.Type == filter._type);

            // Exact Subtype
            if (!string.IsNullOrWhiteSpace(filter.Subtype)) query = query
                    .Where(a => a.Subtype == filter._subtype);

            // Exact Iso_Currency_Code
            if (!string.IsNullOrWhiteSpace(filter.Iso_Currency_Code)) query = query
                    .Where(a => a.Balances!.Iso_Currency_Code == filter.Iso_Currency_Code);

            return query;
        }
    }
}
