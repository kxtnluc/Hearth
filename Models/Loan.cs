using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
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
        public decimal Principal { get; set; }
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
        public double Total_Loan_Amount_C
        {
            get
            {
                var result = 0.00;
                result = Payment_C * Number_Of_Payments_Total_C;
                return result;
            }
        }
        [NotMapped]
        public int Compound_Frequency_C
        {
            get
            {
                var result = 0;
                // TODO
                return result;
            }
        }
        [NotMapped]
        public double Payment_C
        {
            get
            {
                // TODO: Figure out how to add compounding into this calculation
                var result = 0.00;
                if (Principal <= 0 || Interest_Rate < 0 || Term <= 0)
                {
                    throw new ArgumentException("Principal, annual interest rate, and loan term must be positive values.");
                }

                // Calculate the monthly interest rate
                double monthlyInterestRate = (double)Interest_Rate / 12;
                double principal = (double)Principal;
                // If the interest rate is 0, the payment is simply principal / loanTermInMonths
                if (monthlyInterestRate == 0)
                {
                    result = principal / Term;
                    return result;
                }

                // Calculate the monthly payment using the annuity formula
                // M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]
                // Where:
                // M = Monthly payment
                // P = Principal loan amount
                // i = Monthly interest rate
                // n = Total number of payments (loan term in months)
                double numerator = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, Term);
                double denominator = Math.Pow(1 + monthlyInterestRate, Term) - 1;

                result = principal * (numerator / denominator);

                return result;
            }
        }
        [NotMapped]
        public decimal? Downpayment_Paid_C { get; set; }
        [NotMapped]
        public int Number_Of_Payments_Total_C
        {
            get
            {
                var paymentsPerMonth = Payment_Frequency switch
                {
                    CALENDAR_RATE.Annually => 1.0 / 12.0,      // 1 payment per year = 1/12 per month
                    CALENDAR_RATE.Semi_Annually => 2.0 / 12.0, // 2 payments per year = 1/6 per month
                    CALENDAR_RATE.Quarterly => 4.0 / 12.0,     // 4 payments per year = 1/3 per month
                    CALENDAR_RATE.Monthly => 1,                 // 1 payment per month
                    CALENDAR_RATE.Semi_Monthly => 2,            // 2 payments per month
                    CALENDAR_RATE.Biweekly => 26.0 / 12.0,     // 26 payments per year ≈ 2.17 per month
                    CALENDAR_RATE.Weekly => 52.0 / 12.0,       // 52 payments per year ≈ 4.33 per month
                    CALENDAR_RATE.Daily => 365.0 / 12.0,       // 365 payments per year ≈ 30.42 per month
                    _ => 1,
                };

                int result = (int)(Term * paymentsPerMonth);

                return result;
            }
        }
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
                result = (Total_Paid / (decimal)Total_Loan_Amount_C);
                return result;
            }
        }
        [NotMapped]
        public decimal Percent_Of_Loan_Remaining_C
        {
            get
            {
                var result = 0.00M;
                result = 1 - (Total_Paid / (decimal)Total_Loan_Amount_C);
                return result;
            }
        }
        [NotMapped]
        public LOAN_STATUS Loan_Status_C
        {
            get
            {
                var result = LOAN_STATUS.Current;
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

    public enum LOAN_STATUS
    {
        Current,
        Delinquent,
        Defaulted,
        Paid_Off
    }
    #endregion
}
