using Hearth.Foundation.Enums;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.CTOs.Finance.Account
{
    public class AccountReportCTO
    {
        public AccountDTO Account { get; set; }
        public List<TransactionDTO> Transactions { get; set; }
        public bool IncludeTransfers { get; set; } = true;
        public E_CALENDAR_FREQUENCY Period { get; set; } = E_CALENDAR_FREQUENCY.Monthly;
        public decimal OutgoingPerPeriod_Num_C
        {
            get
            {
                var result = 0;

                var transactions = Transactions.Where(t => t.IsIncome_C == false);


                return result;
            }
        }
        public decimal IncomingPerPeriod_Num_C
        {
            get
            {
                var result = 0;

                return result;
            }
        }
        public decimal IncomingPerPeriod_Percent_C
        {
            get
            {
                var result = 0;

                return result;
            }
        }
        public decimal OutgoingPerPeriod_Percent_C
        {
            get
            {
                var result = 0;

                return result;
            }
        }
    }
}
