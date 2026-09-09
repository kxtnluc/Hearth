using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility.Finance
{
    public static class FinanceCalculator
    {
        public static decimal AveragePerPeriod(int periodCount, decimal sum)
        {
            var result = (decimal)0;
            if (periodCount == 0) return 0;

            result = sum / periodCount;

            return result;
        }
    }
}
