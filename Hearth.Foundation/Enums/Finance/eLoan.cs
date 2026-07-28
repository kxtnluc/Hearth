using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Foundation.Enums.Finance
{
    public enum E_LOAN_TYPE
    {
        Normal,
        Mortgage,
        Car_Loan,
        Student_Loan,
        Family_Or_Friend_Loan,
        Business_Loan
    }

    public enum E_LOAN_STATUS
    {
        Current,
        Delinquent,
        Defaulted,
        Paid_Off
    }
}
