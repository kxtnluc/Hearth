using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Foundation.Enums
{
    public enum E_OPERATOR
    {
        [Description("=")]
        Equals = 0,

        [Description(">")]
        GreaterThan = 1,

        [Description("<")]
        LessThan = 2,

        [Description(">=")]
        GreaterThanOrEqualTo = 3,

        [Description("<=")]
        LessThanOrEqualTo = 4,

        [Description(">= <=")]
        Between = 5,

        [Description("+/-")]
        GiveOrTake = 6,

        [Description("!=")]
        NotEquals = 7,
    }
}
