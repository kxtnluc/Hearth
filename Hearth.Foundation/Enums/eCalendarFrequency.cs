using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Foundation.Enums
{
    /// <summary>
    /// Anually, Semi_Anually, Quarterly, Monthly, etc.
    /// </summary>
    public enum E_CALENDAR_FREQUENCY
    {
        [Description("yr")]
        Annually,
        [Description("6-mo")]
        Semi_Annually,
        [Description("qtr")]
        Quarterly,
        [Description("mo")]
        Monthly,
        [Description("2x-mo")]
        Semi_Monthly,
        [Description("bi-weekly")]
        Biweekly,
        [Description("week")]
        Weekly,
        [Description("day")]
        Daily,
        Continuously
    }
}
