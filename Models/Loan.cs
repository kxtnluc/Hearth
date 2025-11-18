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
        public decimal Inital_Principal { get; set; }
        public decimal Principal { get; set; } // The Current Principal
        public int Term { get; set; }
        public decimal Interest_Rate { get; set; }
        public CALENDAR_RATE Compound { get; set; }
        public CALENDAR_RATE Payment_Frequency { get; set; }
        public DateTime? Due_Date { get; set; } = null;
        public DateTime? Start_Date { get; set; }
        #endregion
        #region Current Loan Status Props
        public decimal Principal_Paid { get; set; } = 0;
        public decimal Interest_Paid { get; set; } = 0;
        public decimal? Downpayment { get; set; }
        public bool Active { get; set; } = true;
        #endregion

        #region Calculated Props
        [NotMapped]
        public decimal Remaining_Principal_C // TODO get rid of this because now I have "Current Prinicpal"
        {
            get
            {
                var result = 0.00M;
                result = Principal - Principal_Paid;
                return result;
            }
        }
        [NotMapped]
        public decimal Total_Paid_C
        {
            get
            {
                var result = 0.00M;
                result = Principal_Paid + Interest_Paid;
                return result;
            }
        }
        [NotMapped]
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
        [NotMapped]
        public decimal Monthly_Interest_Rate_C
        {
            get
            {
                var result = 0.00M;
                result = Interest_Rate / 12;
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
        public double Percent_Of_Loan_Paid_C
        {
            get
            {
                var result = 0.00;
                result = ((double)Total_Paid_C / Total_Loan_Amount_C);
                return result;
            }
        }
        [NotMapped]
        public double Percent_Of_Loan_Remaining_C
        {
            get
            {
                var result = 0.00;
                result = 1 - ((double)Total_Paid_C / Total_Loan_Amount_C);
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

        [NotMapped]
        public List<PrincipalToInterestPortion> Portions_C
        {
            get
            {
                var result = new List<PrincipalToInterestPortion>();
                result = LoanFunctions.GetPrincipalAndInterestPortions(this).ToList();
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

    #region Loan Functions
    public class LoanFunctions
    {
        public static decimal EstimatePaymentPrincipleOrInterestByPeriodOfTerms(
            Loan loan,
            int totalPeriods, // (10)
            int period, // (3)
            bool isInterest = false,
            bool isPercentage = false
        )
        {
            var result = 0.00M;

            // Find the period range
            var monthsInAPeriod = loan.Term / totalPeriods; // (36)
            var startIndex = (period - 1) * monthsInAPeriod; // (72)
            // Find the theoretical average principal/interest paid for this period
            if(isInterest)
            {
                if(isPercentage)
                {
                    result = loan.Portions_C
                        .Skip(startIndex)
                        .Take(monthsInAPeriod)
                        .Average(p => p.Percent_Interest_C);
                }
                else
                {
                    result = loan.Portions_C
                        .Skip(startIndex)
                        .Take(monthsInAPeriod)
                        .Average(p => p.Interest_Portion);
                }
            }
            else
            {
                if(isPercentage)
                {
                    result = loan.Portions_C
                        .Skip(startIndex)
                        .Take(monthsInAPeriod)
                        .Average(p => p.Percent_Principal_C);
                }
                else
                {
                    result = loan.Portions_C
                        .Skip(startIndex)
                        .Take(monthsInAPeriod)
                        .Average(p => p.Principal_Portion);
                }
            }

            // return final result o:
            return result;
        }

        public static List<PrincipalToInterestPortion> GetPrincipalAndInterestPortions(Loan loan)
        {
            // TODO there needs to be something in this function that
            // checks if an extra payment has been made,
            // and if so, adjust the remaining principal accordingly.


            var result = new List<PrincipalToInterestPortion>();
            // ---
            var remainPrincipal = loan.Principal;
            for (int i = 0; i < loan.Term; i++) // TODO: Optimize
            {
                var singlePortion = new PrincipalToInterestPortion();
                // Calculate interest portion
                var interestPortion = remainPrincipal * (loan.Monthly_Interest_Rate_C);
                singlePortion.Interest_Portion = interestPortion;
                // Calculate principal portion
                var principalPortion = (decimal)loan.Payment_C - interestPortion;
                singlePortion.Principal_Portion = principalPortion;
                // Add to List
                result.Add(singlePortion);
                // Decrease remaining principal
                remainPrincipal -= principalPortion;
            }
            // ---
            return result;
        }
        #endregion
    }

    public class PrincipalToInterestPortion
    {
        public decimal Principal_Portion { get; set; }
        public decimal Interest_Portion { get; set; }
        public decimal Total_C
        {
            get
            {
                return Principal_Portion + Interest_Portion;
            }
        }
        public decimal Percent_Principal_C
        {
            get
            {
                if (Total_C == 0) return 0;
                return Principal_Portion / Total_C;
            }
        }

        public decimal Percent_Interest_C
        {
            get
            {
                if (Total_C == 0) return 0;
                return Interest_Portion / Total_C;
            }
        }

    }
}
