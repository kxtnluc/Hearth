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
        public static int GetPeriodCount(E_CALENDAR_FREQUENCY period, DateTime start, DateTime end)
        {
            var result = 0;

            if (end < start)
            {
                (start, end) = (end, start);
            }

            int count = period switch
            {
                E_CALENDAR_FREQUENCY.Daily =>
                    (end.Date - start.Date).Days + 1,

                E_CALENDAR_FREQUENCY.Weekly =>
                    (GetIsoWeekBucket(end) - GetIsoWeekBucket(start)) + 1,

                E_CALENDAR_FREQUENCY.Monthly =>
                    ((end.Year - start.Year) * 12 + end.Month - start.Month) + 1,

                E_CALENDAR_FREQUENCY.Annually =>
                    (end.Year - start.Year) + 1,

                _ => throw new NotSupportedException($"Unsupported period: {period}")
            };

            result = count;

            return result;
        }

        /// <summary>
        /// Converts a date string to a long date format with an ordinal suffix for the day (e.g., "January 1st, 2024"). If the input string cannot be parsed as a date, it returns the original string.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns>October 17th, 2025</returns>
        public static string ToLongDateWithOrdinal(string dateString)
        {
            if (!DateTime.TryParse(dateString, out var date))
                return dateString; // fallback: return original string if it doesn't parse

            var day = date.Day;
            var suffix = GetOrdinalSuffix(day);

            return $"{date:MMMM} {day}{suffix}, {date:yyyy}";
        }

        /// <summary>
        /// Converts a date string to a short date format with an ordinal suffix for the day (e.g., "Jan 1st, 2024"). If the input string cannot be parsed as a date, it returns the original string.
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static string ToShortDateWithOrdinal(string dateString)
        {
            if (!DateTime.TryParse(dateString, out var date))
                return dateString; // fallback: return original string if it doesn't parse

            var day = date.Day;
            var suffix = GetOrdinalSuffix(day);

            return $"{date:MMM} {day}{suffix}, {date:yyyy}";
        }

        private static string GetOrdinalSuffix(int day)
        {
            if (day is >= 11 and <= 13) return "th"; // 11th, 12th, 13th are always "th", not "st"/"nd"/"rd"

            return (day % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }

        private static int GetIsoWeekBucket(DateTime date)
        {
            // Combine year and ISO week number into one comparable integer bucket
            var year = System.Globalization.ISOWeek.GetYear(date);
            var week = System.Globalization.ISOWeek.GetWeekOfYear(date);
            return (year * 52) + week;
        }

        /// <summary>
        /// Returns the start and end (inclusive) of the fiscal year containing the reference date.
        /// With startMonth = 1 and startDay = 1 this is the calendar year.
        /// </summary>
        public static (DateTime Start, DateTime End) GetFiscalYearRange(int startMonth, int startDay, DateTime reference)
        {
            DateTime StartForYear(int year) =>
                new DateTime(year, startMonth, Math.Min(startDay, DateTime.DaysInMonth(year, startMonth)));

            var start = StartForYear(reference.Year);

            // Before this year's start date, so we're still in the fiscal year that began last year
            if (reference.Date < start)
            {
                start = StartForYear(reference.Year - 1);
            }

            var end = StartForYear(start.Year + 1).AddDays(-1);

            return (start, end);
        }
    }
}
