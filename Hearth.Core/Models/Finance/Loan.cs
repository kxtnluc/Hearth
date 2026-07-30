using Hearth.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Hearth.Foundation.Enums;
using Hearth.Foundation.Enums.Finance;

namespace Hearth.Core.Models.Finance
{
    public class Loan
    {
        #region Generic Props
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        #endregion
        #region Model Foundational Props
        public E_LOAN_TYPE Loan_Type { get; set; } = E_LOAN_TYPE.Normal;
        public bool Amortized { get; set; } = true;
        public decimal Principal { get; set; } // The Inital Principal (Principal_Current_C is the Current Principal)
        public int Term { get; set; }
        public decimal Interest_Rate { get; set; }
        public E_CALENDAR_FREQUENCY Compound { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
        public E_CALENDAR_FREQUENCY Payment_Frequency { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
        public DateTime? Due_Date { get; set; } = null;
        public DateTime? Start_Date { get; set; }
        public decimal? Downpayment { get; set; }
        #endregion
        #region Loan Status Props
        public decimal Principal_Paid { get; set; } = 0;
        public decimal Interest_Paid { get; set; } = 0;
        public bool Active { get; set; } = true;
        #endregion
    }
}
