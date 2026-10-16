# CTO Template (Finance)

A CTO (Calculated Temporary Object) takes in one or more keystone objects (usually a list of DTOs, plus an optional single selected DTO) and exposes read-only getters that do math across that data: sums, averages, percentages, groupings.
CTOs are never stored. They are built in memory, used for display, and thrown away.

Simply CTRL-H the key tags below, or go through manually, replacing what you need for the given CTO.
Variables marked with a `__` prefix/suffix are meant to be replaced.

### Full Template

```csharp
using Hearth.Foundation.Enums;
using Hearth.Services.DTOs.Finance.__Object;
using Hearth.Services.Utility;
using Hearth.Services.Utility.Finance;

namespace Hearth.Services.CTOs.Finance.__Object
{
    public class __objectListCTO
    {
        #region Primary
        // Keystone Objects
        public List<__objectDTO> __objects { get; set; } = new();
        public __objectDTO? Single__object { get; set; } = null;
        // CTO Options
        public __objectListCTO_O Options { get; set; } = new __objectListCTO_O();
        #endregion

        #region Calculated (List)
        /// <summary>
        /// Example: a sum across the whole list.
        /// </summary>
        public decimal Sum___value_C
        {
            get
            {
                if (__objects.Count == 0) return 0.00m;

                return __objects.Sum(o => o.__value);
            }
        }

        /// <summary>
        /// Example: an average per period, using the earliest date in the list as the start.
        /// </summary>
        public decimal Average___valuePerPeriod_Num_C
        {
            get
            {
                if (__objects.Count == 0) return 0.00m;

                DateTime earliestDate = __objects
                    .Select(o => DateTime.TryParse(o.__date, out var date) ? date : DateTime.MaxValue)
                    .DefaultIfEmpty(DateTime.MaxValue)
                    .Min();

                var periodCount = DateTimeHelper.GetPeriodCount(Options.Frequency, earliestDate, DateTime.Now);

                return FinanceCalculator.AveragePerPeriod(periodCount, Sum___value_C);
            }
        }

        /// <summary>
        /// Example: a grouped sum, keyed by an id.
        /// </summary>
        public Dictionary<int, decimal> Sum___valueBy__group_C
        {
            get
            {
                return __objects
                    .Where(o => o.__groupId != null)
                    .GroupBy(o => o.__groupId!.Value)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Sum(o => o.__value)
                    );
            }
        }
        #endregion

        #region Calculated (Single)
        /// <summary>
        /// Example: the selected item's share of the whole list, as a fraction (0.25 = 25%).
        /// </summary>
        public decimal Single___valueInRelationToList_Percent_C
        {
            get
            {
                if (Single__object == null) return 0.00m;
                if (Sum___value_C == 0) return 0.00m;

                return Single__object.__value / Sum___value_C;
            }
        }
        #endregion

        #region Formated
        private __objectListCTO_F? _formated;
        public __objectListCTO_F Formated => _formated ??= new __objectListCTO_F(this);
        #endregion
    }

    /// <summary>
    /// Formatted (_S) display strings for every calculated (_C) value on the CTO.
    /// </summary>
    public class __objectListCTO_F
    {
        private readonly __objectListCTO _calculated;

        public __objectListCTO_F(__objectListCTO calculated)
        {
            _calculated = calculated;
        }

        public string Sum___value_S =>
            CurrencySymbolHelper.ConvertDecimalToCurrency(_calculated.Sum___value_C, "USD");

        public string Single___valueInRelationToList_Percent_S =>
            _calculated.Single___valueInRelationToList_Percent_C.ToString("P2");
    }

    /// <summary>
    /// Options that change how the CTO calculates.
    /// </summary>
    public class __objectListCTO_O
    {
        public E_CALENDAR_FREQUENCY Frequency { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
    }
}
```

### Usage

```csharp
var stats = new TransactionListCTO
{
    Transactions = filteredTransactions,
    Options = new TransactionListCTO_O { Frequency = E_CALENDAR_FREQUENCY.Monthly }
};
```

```razor
<div>@stats.Formated.Average_OutgoingPerPeriod_Num_S</div>
<ProgressBar Complete="@stats.Sum_Outgoing_C" Total="@stats.Sum_Incoming_C" />
```

Swap the keystone list (e.g. after filtering) and every getter recalculates on the next render. No refresh call needed.

## Placeholders to Replace

| Placeholder | Description |
| :---------: | ----------: |
| `__Object` | Folder/namespace name for the entity (e.g. `Transaction`, `Account`) |
| `__object` | The entity name used in class names (e.g. `Transaction` → `TransactionListCTO`) |
| `__objects` | The keystone list property (e.g. `Transactions`) |
| `__objectDTO` | The DTO type the CTO calculates over (e.g. `TransactionDTO`) |
| `Single__object` | The optional selected item (e.g. `SingleTransaction`). Remove if the CTO never focuses on one item |
| `__value` | The numeric field being calculated on (e.g. `Amount`) |
| `__date` | The date field used for period math (e.g. `Date`) |
| `__group` / `__groupId` | The field used for grouping (e.g. `Category` / `CategoryId`) |
| `"USD"` | Currency code for formatting. Replace with the real code from the data (e.g. a `CurrencyCode_S` on the DTO) |

## Structure Notes

| Section | Description |
| :---------: | ----------: |
| Primary | Keystone objects (the data the CTO calculates over) and the Options object. These are the only settable properties |
| Calculated (List) | `_C` getters that calculate across the whole keystone list: sums, averages, groupings |
| Calculated (Single) | `_C` getters that compare the single selected item against the list. Always return `0` when nothing is selected |
| Formated | A lazily-created `_F` class that turns `_C` numbers into `_S` display strings. The backing field stays `private` |
| `_F` class | Holds a reference to the CTO and reads its `_C` values. Never calculates anything itself, only formats |
| `_O` class | Settings that change how values are calculated (frequency, include transfers, etc.) |

## Conventions

| Rule | Description |
| :---------: | ----------: |
| `_C` / `_S` / `_F` / `_O` | Calculated value / formatted string / formatted class / options class |
| Getters only | CTOs never store results. Every `_C` recalculates from the keystone data on each read |
| Guard divisions | Return `0` when a list is empty or a divisor is `0`. Never let a getter throw on empty data |
| Percents are fractions | Store `0.25` for 25% and format with `ToString("P2")`. Don't multiply by 100 in the `_C` |
| No database access | CTOs only work on data handed to them. Fetching happens in the service (`CTO Generation` region) |
| Heavy getters | Getters run on every access. If one is expensive and read many times per render, compute it once into a local variable in the page |