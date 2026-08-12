using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Foundation.Enums
{
    public enum E_CONDITION
    {
        [Description("=")]
        Equals,
        [Description(">")]
        GreaterThan,
        [Description("<")]
        LessThan,
        [Description("≥")]
        GreaterThanOrEqualTo,
        [Description("≤")]
        LessThanOrEqualTo,
        [Description("≥ ≤")]
        Between,
        [Description("Exact")]
        Exact,
        [Description("Starts With")]
        StartsWith,
        [Description("Ends With")]
        EndsWith,
        [Description("Contains")]
        Contains,
        //[Description("Regex")]
        //Regex
    }
}
