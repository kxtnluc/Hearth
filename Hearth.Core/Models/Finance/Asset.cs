// HEARTH/FINANCE
using Hearth.Core.Data;
using Hearth.Foundation.Enums;
using Hearth.Foundation.Enums.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hearth.Core.Models.Finance
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
        public E_ASSET_TYPE Asset_Type { get; set; } = E_ASSET_TYPE.Other;
        public E_CALENDAR_FREQUENCY Compound_Rate { get; set; } = E_CALENDAR_FREQUENCY.Annually;
        public DateTime Purchase_Date { get; set; }
        #endregion
        #region Joined Props
        public int? LoanId { get; set; }
        #endregion
    }
}
