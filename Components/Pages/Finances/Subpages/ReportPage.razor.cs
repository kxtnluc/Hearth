using ApexCharts;
using Hearth.Models;
using Hearth.Models.CTO;
using Microsoft.AspNetCore.Components;

namespace Hearth.Components.Pages.Finances.Subpages
{
    public partial class ReportPage : ComponentBase
    {
        #region Variables

        private bool initialRenderComplete = false;

        ReportCTO report = new ReportCTO();

        List<Transaction>? allTransactions = new List<Transaction> { };
        List<Transaction>? transactionsInRange = new List<Transaction> { };

        private int inputFromMonth = DateTime.Now.Month;
        private int inputFromYear = DateTime.Now.Year;
        private int inputToMonth;
        private int inputToYear;

        private DateTime inputFromDate;
        private DateTime inputToDate;

        // Creates a List of years from 1900 to the current year
        List<int> years = Enumerable
            .Range(1900, DateTime.Now.Year - 1900 + 1)
            .Reverse()
            .ToList();

        // List of all 12 months
        List<int> months = new List<int>
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
        };
        // Apex Charts

        private string chartType = "category";

        private class ReportSlice
        {
            public string Label { get; set; }
            public decimal Value { get; set; }
        }
        private ApexChartOptions<ReportSlice> options { get; set; } = new();
        private ApexChart<ReportSlice> reportChart;

        private List<ReportSlice> reportData = new()
        {
            new ReportSlice { Label = "Expenses", Value = 0 },
            new ReportSlice { Label = "Profit", Value = 0 },
        };

        private List<ReportSlice> reportDataCategories = new()
        {

        };

        private bool _chartInitialized = false;

        #endregion /Variables

        #region Functions

        #region On Page Load
        protected override async Task OnInitializedAsync()
        {
            GenerateReport();
            SetChartOptions();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _chartInitialized = true;
                UpdateChart(report);

                if (initialRenderComplete == false)
                {
                    initialRenderComplete = true;
                    StateHasChanged(); // Trigger re-render to hide the spinner
                }
            }
        }
        #endregion /On Page Load

        #region GET Transactions
        private async void GetAndSetAllTransactions()
        {
            try
            {
                // Gets all the transactions from the db
                allTransactions = TransactionFunctions.GetAllUnignoredTransactions(_dbContext);
                transactionsInRange = _qolService.SetTransactionsToTimeRange(allTransactions, inputFromDate, inputToDate); // change this to class function
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong when trying to GET data from _dbContext.Transactions" + ex.Message);
            }
        }
        #endregion /GET Transactions

        #region Calculate Report

        private void GenerateReport()
        {
            GenerateInputDates();
            GetAndSetAllTransactions();
            report = _qolService.CalculateReport(transactionsInRange, inputFromDate, inputToDate);
            if (_chartInitialized) UpdateChart(report);
        }

        private async void UpdateChart(ReportCTO r)
        {
            switch (chartType)
            {
                case "profit":
                    UpdateProfitChart(r);
                    break;
                case "category":
                    UpdateCategoriesChart(r);
                    break;
            }


            // Basically redraw EVERYthing
            await reportChart.UpdateOptionsAsync(true, true, true);
            await reportChart.UpdateSeriesAsync();

        }
        private void SetChartOptions(bool showLegend = false, bool showDropShadow = false, string[]? colors = null)
        {
            if (colors == null)
            {
                colors = new[] { "#00ABFF", "#00C441" };

            }

            options.DataLabels = new DataLabels { DropShadow = new DropShadow { Enabled = showDropShadow } };
            options.Legend = new Legend { Show = showLegend };
            options.Colors = colors.ToList();
        }

        #endregion
        #region Handlers
        private void NextMonth()
        {
            inputFromMonth++;
            GenerateReport();
        }
        private void PreviousMonth()
        {
            inputFromMonth--;
            GenerateReport();
        }
        #endregion Handlers
        #region Calculations
        private void GenerateInputDates()
        {
            // If a FROM is set
            if (inputFromMonth != 0 && inputFromYear != 0)
            {
                // set FROM
                inputFromDate = new DateTime(inputFromYear, inputFromMonth, 1);
            }
            // If a TO is set
            if (inputToMonth != 0 && inputToYear != 0)
            {
                // set TO
                inputToDate = new DateTime(inputToMonth, inputToYear, 1);
            }
            // If ONLY a FROM is set
            else
            {
                // set TO to 1 month ahead of FROM
                inputToDate = inputFromDate.AddMonths(1);
            }

            return;
        }
        #endregion

        private void UpdateCategoriesChart(ReportCTO r)
        {

            reportDataCategories.Clear();

            List<string> colorList = new List<string>();

            foreach (var c in r.Category_Spending_Map)
            {
                Category category = _dbContext.Categories.SingleOrDefault(dbc => dbc.Id == c.CategoryId);

                if (category != null && !category.Income) // Category is NOT income
                {
                    reportDataCategories.Add(new ReportSlice
                    {
                        Label = category.Name,
                        Value = Math.Abs(c.TotalAmount)
                    });

                    colorList.Add(category.Hex_Color);

                }
            }

            string[] colors = colorList.ToArray();
            SetChartOptions(false, false, colors);

        }

        private void UpdateProfitChart(ReportCTO r)
        {
            // Setting Graph Data
            var expenses = r.Expenses_C;
            var profit = r.Profit_C;


            reportData[0].Value = expenses;

            if (profit < 0)
            {

                reportData[1].Label = "Loss";
                reportData[1].Value = Math.Abs(profit);
                string[] colors = { "#00ABFF", "#FF8360" };
                SetChartOptions(false, false, colors);
            }
            else
            {
                reportData[1].Label = "Profit";
                reportData[1].Value = profit;
                string[] colors = { "#00ABFF", "#00C441" };
                SetChartOptions(false, false, colors);
            }
        }

        #endregion /Functions
    }
}