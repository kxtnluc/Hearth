using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Foundation.Enums
{
    public enum E_OPERATOR
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
        [Description("+/-")]
        GiveOrTake,
    }
}
