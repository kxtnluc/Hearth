using Hearth.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Gets the number of periods between two dates based on the specified frequency. ex: 12 periods in a year for monthly, 52 periods in a year for weekly, etc.
        /// </summary>
        /// <param name="period"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static int GetPeriodCount(E_CALENDAR_FREQUENCY period, DateTime start, DateTime end, bool inclusive = false)
        {
            var result = 0;

            if (end < start)
            {
                (start, end) = (end, start);
            }

            int count = period switch
            {
                E_CALENDAR_FREQUENCY.Daily =>
                    (end.Date - start.Date).Days,

                E_CALENDAR_FREQUENCY.Weekly =>
                    (end.Date - start.Date).Days / 7,

                E_CALENDAR_FREQUENCY.Monthly =>
                    ((end.Year - start.Year) * 12) + end.Month - start.Month,

                E_CALENDAR_FREQUENCY.Annually =>
                    end.Year - start.Year,

                _ => throw new NotSupportedException($"Unsupported period: {period}")
            };

            result = inclusive ? count + 1 : count;

            return result;
        }
    }
}
