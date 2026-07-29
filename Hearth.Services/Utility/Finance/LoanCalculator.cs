using Hearth.Services.DTOs.Finance.Loan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility.Finance
{
    public static class LoanCalculator
    {
        /// <summary>
        /// Estimates the average principal or interest paid per month, within one segment
        /// of a loan's term, when the full term is split into equal-sized periods.
        /// </summary>
        /// <param name="loan">The loan whose amortization schedule (Portions_C) is being segmented.</param>
        /// <param name="totalPeriods">How many equal segments to split the loan's full term into (e.g. 10).</param>
        /// <param name="period">Which segment to compute the average for, 1-indexed (e.g. 3 = third segment).</param>
        /// <param name="isInterest">If true, averages the interest portion; if false, averages the principal portion.</param>
        /// <param name="isPercentage">If true, averages the percentage-of-payment value instead of the dollar amount.</param>
        public static decimal EstimatePaymentPrincipleOrInterestByPeriodOfTerms(
            LoanDTO loan,
            int totalPeriods,
            int period,
            bool isInterest = false,
            bool isPercentage = false
        )
        {
            var result = 0.00M;

            // Split the loan's full term into equal-sized segments.
            // e.g. a 36-month loan split into 12 periods = 3 months per segment.
            var monthsInAPeriod = loan.Term / totalPeriods;

            // Convert the 1-indexed period into a 0-indexed starting month offset.
            // e.g. period 3 with 3-month segments starts at month index 6.
            var startIndex = (period - 1) * monthsInAPeriod;

            // Pull just the months belonging to this segment, then average
            // whichever value the caller asked for (interest/principal, $ or %).
            var segment = loan.Portions_C
                .Skip(startIndex)
                .Take(monthsInAPeriod);

            if (isInterest)
            {
                result = isPercentage
                    ? segment.Average(p => p.Percent_Interest_C)
                    : segment.Average(p => p.Interest_Portion);
            }
            else
            {
                result = isPercentage
                    ? segment.Average(p => p.Percent_Principal_C)
                    : segment.Average(p => p.Principal_Portion);
            }

            return result;
        }

        public static List<PrincipalToInterestPortion> GetPrincipalAndInterestPortions(LoanDTO loan)
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
        // Helper Classes
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
}
