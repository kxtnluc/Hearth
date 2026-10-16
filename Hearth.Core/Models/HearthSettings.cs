using Hearth.Core.Interfaces;

namespace Hearth.Core.Models
{
    public class HearthSettings : ISqliteTable
    {
        public int Id { get; set; }
        public string PresetName { get; set; } = "default";
        // Hearth Settings

        // Hearth/Finance Settings
        public bool UseCategoryWeights { get; set; } = true;
        public bool UseRulePriorities { get; set; } = true;
        /// <summary>
        /// Month the user's fiscal year starts on (1-12). Default 1 = January (calendar year).
        /// </summary>
        public int FiscalYearStartMonth { get; set; } = 1;
        /// <summary>
        /// Day of the month the fiscal year starts on (1-31). Default 1.
        /// </summary>
        public int FiscalYearStartDay { get; set; } = 1;
    }
}