using Hearth.Foundation.Enums;
using Hearth.Foundation.Enums.Finance;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Hearth.Services.Utility.Finance.LoanCalculator;


namespace Hearth.Services.DTOs.Finance.Loan
{
    public class LoanDTO
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
        #region Calculated Props
        [MapperIgnore]
        public decimal Principal_Current_C
        {
            get
            {
                var result = 0.00M;
                // TODO calcualte the number of payments and OVERpayments made to get this value, then plug it in various places
                return result;
            }
        }
        [MapperIgnore]
        public decimal Remaining_Principal_C // TODO get rid of this because now I have "Current Prinicpal"
        {
            get
            {
                var result = 0.00M;
                result = Principal - Principal_Paid;
                return result;
            }
        }
        [MapperIgnore]
        public decimal Total_Paid_C
        {
            get
            {
                var result = 0.00M;
                result = Principal_Paid + Interest_Paid;
                return result;
            }
        }
        [MapperIgnore]
        public double Total_Loan_Amount_C
        {
            get
            {
                var result = 0.00;
                if (Amortized == false || Term == 0)
                {
                    result = Payment_C;
                    return result;
                }
                result = Payment_C * Number_Of_Payments_Total_C;
                return result;
            }
        }
        [MapperIgnore]
        public int Compound_Frequency_C
        {
            get
            {
                var result = 0;
                // TODO
                return result;
            }
        }
        [MapperIgnore]
        public double Payment_C
        {
            get
            {
                // TODO: Figure out how to add compounding into this calculation
                var result = 0.00;
                // If the loan is NOT amortized
                if (Amortized == false)
                {
                    result = (double)Principal + ((double)Principal * (double)Interest_Rate);
                    result = result - (double)Total_Paid_C;
                    return result;
                }
                if (Principal <= 0 || Interest_Rate < 0 || Term < 0)
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
        [MapperIgnore]
        public decimal Monthly_Interest_Rate_C
        {
            get
            {
                var result = 0.00M;
                result = Interest_Rate / 12;
                return result;
            }
        }
        [MapperIgnore]
        public decimal? Downpayment_Paid_C { get; set; }
        [MapperIgnore]
        public int Number_Of_Payments_Total_C
        {
            get
            {
                var paymentsPerMonth = Payment_Frequency switch
                {
                    E_CALENDAR_FREQUENCY.Annually => 1.0 / 12.0,      // 1 payment per year = 1/12 per month
                    E_CALENDAR_FREQUENCY.Semi_Annually => 2.0 / 12.0, // 2 payments per year = 1/6 per month
                    E_CALENDAR_FREQUENCY.Quarterly => 4.0 / 12.0,     // 4 payments per year = 1/3 per month
                    E_CALENDAR_FREQUENCY.Monthly => 1,                 // 1 payment per month
                    E_CALENDAR_FREQUENCY.Semi_Monthly => 2,            // 2 payments per month
                    E_CALENDAR_FREQUENCY.Biweekly => 26.0 / 12.0,     // 26 payments per year ≈ 2.17 per month
                    E_CALENDAR_FREQUENCY.Weekly => 52.0 / 12.0,       // 52 payments per year ≈ 4.33 per month
                    E_CALENDAR_FREQUENCY.Daily => 365.0 / 12.0,       // 365 payments per year ≈ 30.42 per month
                    _ => 1,
                };

                int result = (int)(Term * paymentsPerMonth);

                return result;
            }
        }
        [MapperIgnore]
        public int? Number_Of_Payments_Made_C { get; set; }
        [MapperIgnore]
        public int? Number_Of_Payments_Missed_C { get; set; }
        [MapperIgnore]
        public DateTime Next_Payment_Date_C
        {
            get
            {
                var result = new DateTime();
                // TODO
                return result;
            }
        }
        [MapperIgnore]
        public DateTime Days_Until_Next_Payment_C
        {
            get
            {
                var result = new DateTime();
                // TODO
                return result;
            }
        }
        [MapperIgnore]
        public double Percent_Of_Loan_Paid_C
        {
            get
            {
                var result = 0.00;
                result = ((double)Total_Paid_C / Total_Loan_Amount_C);
                return result;
            }
        }
        [MapperIgnore]
        public double Percent_Of_Loan_Remaining_C
        {
            get
            {
                var result = 0.00;
                result = 1 - ((double)Total_Paid_C / Total_Loan_Amount_C);
                return result;
            }
        }
        [MapperIgnore]
        public E_LOAN_STATUS Loan_Status_C
        {
            get
            {
                var result = E_LOAN_STATUS.Current;
                // TODO
                return result;
            }
        }
        [MapperIgnore]
        public decimal Outstanding_Loan_Balance_C
        {
            get
            {
                var result = 0.00M;
                // TODO
                return result;
            }
        }

        [MapperIgnore]
        public List<PrincipalToInterestPortion> Portions_C
        {
            get
            {
                var result = new List<PrincipalToInterestPortion>();
                result = GetPrincipalAndInterestPortions(this).ToList();
                return result;
            }
        }
        #endregion
    }
}