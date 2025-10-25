using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hearth.Models;
using Hearth.Models.CTO;
using Hearth.Data;
using Microsoft.AspNetCore.Components;

namespace Hearth.Services
{
    public class QolService
    {
        private readonly HearthDbContext _dbContext;

        public QolService(HearthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static async Task<bool> IsUserLoggedIn()
        {
            // Retrieve the session token from secure storage
            var storedToken = "";

            try
            {
                storedToken = await SecureStorage.GetAsync("session_token");

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("An Error occured while fetching your login token: ", ex);
            }
            // Check if a token exists
            if (string.IsNullOrEmpty(storedToken))
            {
                // No token found, user is not logged in
                return false;
            }

            return true; // Token exists, consider the user as logged in
        }

        public static async Task<User> GetActiveUserData()
        {
            var isLoggedIn = await IsUserLoggedIn();

            if (isLoggedIn)
            {
                var storedToken = await SecureStorage.GetAsync("session_token");

                User user = new User { };

                // Split the string by '_'
                var parts = storedToken.Split('_');

                user.Name = parts[3];
                user.Id = int.Parse(parts[2]);
                user.RoleId = int.Parse(parts[4]);

                return user;
            }
            else
            {
                return null;
            }
        }

        public int UpdateDropdown(ChangeEventArgs input, int varToChange)
        {
            if (input.Value != null && !(varToChange < 0))
            {
                varToChange = Int32.Parse(input.Value.ToString()!);
            }

            return varToChange;
        }

        public List<Transaction> SetTransactionsToTimeRange(List<Transaction> transactions, DateTime from, DateTime to)
        {
            // Sets aside those transactions into a list within a time range
            //var transactionsInTimeRange = transactions
            //    .Where(t =>
            //    {
            //        // Attempts to Parse the string into DateTime
            //        if (DateTime.TryParse(t.Date, out var dt))
            //        {
            //            return dt >= from && dt < to;
            //        }
            //        return false;
            //    })
            //    .ToList();

            var transactionsInTimeRange = transactions
                .AsEnumerable() // brings data into memory
                .Where(t =>
                {
                    DateTime date = DateTime.Parse(t.Date);
                    return date >= from && date <= to;
                })
                .ToList();



            return transactionsInTimeRange;
        }
        // Should prob just put this function in the BudgetCTO Class
        public ReportCTO CalculateReport(List<Transaction> transactions, DateTime? from, DateTime? to)
        {

            List<Transaction> previousReportsTransactions = new List<Transaction>();
            // "real" represents the actual date-range between the transactions, whereas non-"real", represents date params passed
            DateTime realFrom = transactions.Any()
                ? transactions.Min(t => DateTime.Parse(t.Date))
                : DateTime.MinValue;


            DateTime realTo = transactions.Any()
                ? transactions.Max(t => DateTime.Parse(t.Date))
                : DateTime.MaxValue;

            double numberOfDays = 0;
            double realNumberOfDays = (realTo - realFrom).TotalDays;
            // Just debug variables below, NOT FINAL
            DateTime previous_realFrom = DateTime.MinValue;
            DateTime previous_realTo = DateTime.MaxValue;
            // -
            if (from.HasValue && to.HasValue)
            {
                // Have to use .Value because to and from are nullable in the params
                numberOfDays = (to.Value - from.Value).Days;

                previousReportsTransactions = _dbContext.Transactions
                    .AsEnumerable() // brings data into memory
                    .Where(t =>
                    {
                        DateTime date = DateTime.Parse(t.Date);
                        return date >= from.Value.AddMonths(-1) && date <= to.Value.AddMonths(-1);
                    })
                    .ToList();

                    previous_realFrom = previousReportsTransactions.Any()
                        ? previousReportsTransactions.Min(t => DateTime.Parse(t.Date))
                        : DateTime.MinValue;


                    previous_realTo = previousReportsTransactions.Any()
                        ? previousReportsTransactions.Max(t => DateTime.Parse(t.Date))
                        : DateTime.MaxValue;
            }
            else if (from == null && to == null)
            {
                numberOfDays = realNumberOfDays;
                // Handle null case
                previousReportsTransactions = _dbContext.Transactions
                        .AsEnumerable() // brings data into memory
                        .Where(t =>
                        {
                            DateTime date = DateTime.Parse(t.Date);
                            return date >= realFrom.AddMonths(-1) && date <= realTo.AddMonths(-1);
                        })
                        .ToList();
            }


            ReportCTO report = new ReportCTO()
            {
                Transactions = transactions,
                Previous_Transactions = previousReportsTransactions,

                From = from,
                To = to,

                Real_From = realFrom,
                Real_To = realTo,

                Is_Primary = true,

                Compared_Report = new ReportCTO()
                {
                    Transactions = previousReportsTransactions,

                    From = from,
                    To = to,

                    Real_From = previous_realFrom,
                    Real_To = previous_realTo,
                }

            };

            return report;
        }
    
        public double[] PlotDataPointsAsPie(double[] dataPoints, bool plotDataAsAbsolute = false)
        {
            double[] result = { };

            double sum = 0;

            for (int i = 0; i < dataPoints.Length; i++)
            {
                // datum is the singular of data o:
                var datum = dataPoints[i];

                if (plotDataAsAbsolute)
                {
                    datum = Math.Abs(datum);
                }

                sum = (sum + datum);

            }

            foreach(double datum in dataPoints)
            {
                var pieceOfThePie = Math.Round((datum / sum),0);

                result
                    .Append(pieceOfThePie)
                    .ToArray();
            }

            return result;
        }
        // AI Generated function below, please test before using
        public static decimal CalculateMedian<T>(IEnumerable<T> list, Func<T, decimal> selector)
        {
            if (list == null || !list.Any())
                return 0;

            var sortedValues = list
                .Select(selector)
                .OrderBy(x => x)
                .ToList();

            int count = sortedValues.Count;
            int middle = count / 2;

            if (count % 2 == 0)
            {
                // Even: average the two middle elements
                return (sortedValues[middle - 1] + sortedValues[middle]) / 2;
            }
            else
            {
                // Odd: return the middle element
                return sortedValues[middle];
            }
        }

    }
}
