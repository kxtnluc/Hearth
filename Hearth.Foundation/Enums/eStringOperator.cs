using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Foundation.Enums
{
    public enum E_STRING_OPERATOR
    {
        [Description("exact")]
        Exact = 0,

        [Description("starts-with")]
        StartsWith = 1,

        [Description("ends-with")]
        EndsWith = 2,

        [Description("contains")]
        Contains = 3,

        [Description("is-not-exact")]
        IsNotExact = 4,
    }
}
