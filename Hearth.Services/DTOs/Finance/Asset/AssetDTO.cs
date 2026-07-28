using Hearth.Foundation.Enums;
using Hearth.Foundation.Enums.Finance;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs.Finance.Asset
{
    internal class AssetDTO
    {
        #region Generic Props
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        #endregion
        #region Model Specific Props
        public decimal Purchase_Price { get; set; } // the value the asset was purchased at
        public decimal Expected_Growth_Or_Decay { get; set; } // The percent anually of Value growth or decay.
        public E_ASSET_TYPE Asset_Type { get; set; } = E_ASSET_TYPE.Other;
        public E_CALENDAR_FREQUENCY Compound_Rate { get; set; } = E_CALENDAR_FREQUENCY.Annually;
        public DateTime Purchase_Date { get; set; }
        #endregion
        #region Joined Props
        public int? LoanId { get; set; }
        #endregion
        // Should prob att an AccountId prop too?
        #region Calculated Props
        [MapperIgnore]
        public bool Attached_To_Loan_C
        {
            get
            {
                if (LoanId != null)
                {
                    return true;
                }
                return false;
            }
        }
        [MapperIgnore]
        public bool Is_Appreciating_C
        {
            get
            {
                if (Expected_Growth_Or_Decay > 0)
                {
                    return true;
                }
                return false;
            }
        }
        [MapperIgnore]
        public decimal Evaluated_Current_Price_C
        {
            get
            {
                // TODO Account for different compounding rates later
                var result = 0.00M;
                result = Purchase_Price + Value_Change_Total_C;
                return result;
            }
        }
        [MapperIgnore]
        public decimal Value_Change_Total_C
        {
            get
            {
                var result = 0.00M;
                result = Value_Change_Per_Month_C * Age_In_Months_C;
                return result;
            }
        }
        [MapperIgnore]
        public decimal Value_Change_Per_Month_C
        {
            get
            {
                var result = 0.00M;
                result = Expected_Growth_Or_Decay_Per_Month_C * Purchase_Price;
                return result;
            }
        }
        [MapperIgnore]
        public decimal Value_Change_Per_Year_C
        {
            get
            {
                var result = 0.00M;
                result = Expected_Growth_Or_Decay * Purchase_Price;
                return result;
            }
        }
        [MapperIgnore]
        public int Age_In_Months_C
        {
            get
            {
                var now = DateTime.Now;
                var months = (now.Year - Purchase_Date.Year) * 12 + now.Month - Purchase_Date.Month;
                return months;
            }
        }
        [MapperIgnore]
        public decimal Expected_Growth_Or_Decay_Per_Month_C
        {
            get
            {
                return Expected_Growth_Or_Decay / 12;
            }
        }
        [MapperIgnore]
        public decimal Evaluated_Five_Year_Price_C
        {
            get
            {
                var result = 0.00M;
                result = (Value_Change_Per_Month_C * 60) + Evaluated_Current_Price_C;
                return result;
            }
        }
        #endregion
    }
}
