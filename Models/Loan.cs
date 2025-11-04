using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models
{
    public class Loan
    {
        #region Databse Foundational Props
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        #endregion
        #region Loan Foundational Props
        public LOAN_TYPE Loan_Type { get; set; }
        public bool Amortized { get; set; } = true;
        public decimal Principle { get; set; }
        public int Term { get; set; }
        public decimal Interest_Rate { get; set; }
        public CALENDAR_RATE Compound { get; set; }
        public CALENDAR_RATE Payment_Frequency { get; set; }
        public int? Due_Date { get; set; } = null;
        public int Start_Date { get; set; }
        #endregion
        #region Current Loan Status Props
        public decimal Total_Paid { get; set; } = 0;
        public decimal? Downpayment { get; set; }
        public bool Active { get; set; } = true;
        #endregion

        #region Calculated Props
        [NotMapped]
        public decimal Payment_C { get; set; }
        [NotMapped]
        public decimal? Downpayment_Paid_C { get; set; }
        [NotMapped]
        public int? Number_Of_Payments_Made_C { get; set; }
        [NotMapped]
        public int? Number_Of_Payments_Missed_C { get; set; }
        [NotMapped]
        public DateTime Next_Payment_Date_C
        {
            get
            {
                var result = new DateTime();
                // TODO
                return result;
            }
        }
        [NotMapped]
        public DateTime Days_Until_Next_Payment_C
        {
            get
            {
                var result = new DateTime();
                // TODO
                return result;
            }
        }
        [NotMapped]
        public decimal Percent_Of_Loan_Paid_C
        {
            get
            {
                var result = 0.00M; 
                // TODO
                return result;
            }
        }
        [NotMapped]
        public decimal Outstanding_Loan_Balance_C
        {
            get
            {
                var result = 0.00M;
                // TODO
                return result; 
            }
        }
        #endregion
    }

    #region Loan Related Enums
    public enum CALENDAR_RATE
    {
        Annually,
        Semi_Annually,
        Quarterly,
        Monthly,
        Semi_Monthly,
        Biweekly,
        Weekly,
        Daily,
        Continuously
    }

    public enum LOAN_TYPE
    {
        Regular,
        Mortgage,
        Car_Loan,
        Student_Loan,
        Family_Or_Friend_Loan,
        Business_Loan
    }
    #endregion
}
