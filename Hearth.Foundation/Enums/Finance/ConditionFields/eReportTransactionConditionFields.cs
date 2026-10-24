using Hearth.Foundation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hearth.Foundation.Enums.Finance.ConditionFields
{
    public enum E_REPORT_TRANSACTION_CONDITION_FIELDS
    {
        [Description("Merchant Name")]
        [ConditionField("Merchant_Name", E_FIELD_TYPE.String)]
        Merchant_Name,

        [Description("Amount")]
        [ConditionField("Amount", E_FIELD_TYPE.Number)]
        Amount,

        [Description("Category")]
        [ConditionField("Category.Name", E_FIELD_TYPE.String)]
        Category__Name,

        [Description("Plaid Primary Category")]
        [ConditionField("Personal_Finance_Category.Primary", E_FIELD_TYPE.String)]
        Personal_Finance_Category__Primary,

        [Description("Plaid Detailed Category")]
        [ConditionField("Personal_Finance_Category.Detailed", E_FIELD_TYPE.String)]
        Personal_Finance_Category__Detailed,

        [Description("Pending")]
        [ConditionField("Pending", E_FIELD_TYPE.Boolean)]
        Pending,
    }
}
