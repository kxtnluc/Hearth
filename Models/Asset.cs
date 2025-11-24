using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Hearth.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models
{
    public class Asset
    {
        #region Databse Foundational Props
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        #endregion
        #region Asset Specific Props
        public decimal Purchase_Price { get; set; } // the value the asset was purchased at
        public decimal Expected_Growth_Or_Decay { get; set; } // The percent anually of Value growth or decay.
        public ASSET_TYPE Asset_Type {get ; set; } = ASSET_TYPE.Other;
        public CALENDAR_RATE Compound_Rate { get; set; } = CALENDAR_RATE.Annually;
        public DateTime Purchase_Date { get; set; }
        public int? LoanId { get; set; }
        // Should prob att an AccountId prop too.
        #endregion
        #region Calculated Props
        [NotMapped]
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
        [NotMapped]
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
        [NotMapped]
        public decimal Value_Change_Total_C
        {
            get
            {
                var result = 0.00M;
                result = Value_Change_Per_Month_C * Age_In_Months_C;
                return result;
            }
        }
        [NotMapped]
        public decimal Value_Change_Per_Month_C
        {
            get
            {
                var result = 0.00M;
                result = Expected_Growth_Or_Decay_Per_Month_C * Purchase_Price;
                return result;
            }
        }
        [NotMapped]
        public decimal Value_Change_Per_Year_C
        {
            get
            {
                var result = 0.00M;
                result = Expected_Growth_Or_Decay * Purchase_Price;
                return result;
            }
        }
        [NotMapped]
        public int Age_In_Months_C
        {
            get
            {
                var now = DateTime.Now;
                var months = (now.Year - Purchase_Date.Year) * 12 + now.Month - Purchase_Date.Month;
                return months;
            }
        }
        [NotMapped]
        public decimal Expected_Growth_Or_Decay_Per_Month_C
        {
            get
            {
                return Expected_Growth_Or_Decay / 12;
            }
        }
        [NotMapped]
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

    public enum ASSET_TYPE
    {
        Property,
        Vehicle,
        Collectible, // Like rare items, art, etc
        Stock,
        Bond,
        Cryptocurrency,
        Account,
        Other
    }

    public class AssetFunctions
    {
        public static List<Asset> GetAll(HearthDbContext _dbContext)
        {
            return _dbContext.Assets.ToList();
        }
    }
}
