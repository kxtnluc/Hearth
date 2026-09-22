using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Filters.Finance
{
    public class TransactionFilter : SqliteTableFilter
    {
        public string? Transaction_Id { get; set; } = null;
        public string? Transaction_Name { get; set; } = null;
        public string? Merchant_Name { get; set; } = null;
        public string? Payment_Channel { get; set; } = null;
        public string? FromDate { get; set; } = null;
        public string? ToDate { get; set; } = null;
        public decimal? Amount_Min { get; set; } = null;
        public decimal? Amount_Max { get; set; } = null;
        public bool IncomeOnly { get; set; } = false;
        public string? Iso_Currency_Code { get; set; } = null;

        /// <summary>
        /// Type is a Dropdown, so is treated differently
        /// </summary>
        public string? Account_Id
        {
            get => _account_id;
            set => _account_id = FilterHelper.NormalizeAnyOption(value);
        }
        private string? _account_id;
        public string? Bank { get; set; } = null; //idk how tf im gonna handle this one
        public static IQueryable<TransactionDTO> BuildQuery(IQueryable<TransactionDTO> query, TransactionFilter filter)
        {
            // Exact Account Id Search
            if (filter.Transaction_Id != null) query = query
                    .Where(a => a.Transaction_Id == filter.Transaction_Id);

            // Soft/partial Name match
            if (!string.IsNullOrWhiteSpace(filter.Transaction_Name))
                query = query.Where(t =>
                    t.Name != null &&
                    t.Name.Contains(filter.Transaction_Name, StringComparison.OrdinalIgnoreCase));

            // Soft/partial Merchant_Name match
            if (!string.IsNullOrWhiteSpace(filter.Merchant_Name))
                query = query.Where(t =>
                    t.Merchant_Name != null &&
                    t.Merchant_Name.Contains(filter.Merchant_Name, StringComparison.OrdinalIgnoreCase));

            // Income Only
            if(filter.IncomeOnly) query = query.Where(t => t.IsIncome_C);

            // Amount is >= Min
            if (filter.Amount_Min != null)
                query = query.Where(t => t.Amount >= filter.Amount_Min);
            // Amount is <= Max
            if (filter.Amount_Max != null)
                query = query.Where(a => a.Amount <= filter.Amount_Max);

            // Exact Type
            if (!string.IsNullOrWhiteSpace(filter.Account_Id)) query = query
                    .Where(a => a.Account_Id == filter._account_id);

            // Exact Iso_Currency_Code
            if (!string.IsNullOrWhiteSpace(filter.Iso_Currency_Code)) query = query
                    .Where(t => t.Iso_Currency_Code == filter.Iso_Currency_Code);

            return query;
        }
    }
}
