using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Enums
{
    public enum E_PLAID_ACCOUNT_TYPES
    {
        [Description("depository")]
        depository,
        [Description("credit")]
        credit,
        [Description("loan")]
        loan,
        [Description("investment")]
        investment,
        [Description("payroll")]
        payroll,
        [Description("other")]
        other
    }
}
