using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance.ValueObjects
{
    public class TransactionPaymentMeta
    {
        public string? By_Order_Of { get; set; }
        public string? Payee { get; set; }
        public string? Payer { get; set; }
        public string? Payment_Method { get; set; }
        public string? Payment_Processor { get; set; }
        public string? Ppd_Id { get; set; }
        public string? Reason { get; set; }
        public string? Reference_Number { get; set; }
    }
}
