using Hearth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs.Finance.Transaction
{
    public class TransactionSyncRecordDTO : IDTO
    {
        public int Id { get; set; }
        public string Next_Cursor { get; set; } = default!;
        public bool Has_More { get; set; }
        public string Request_Id { get; set; } = default!;
        public string Transactions_Update_Status { get; set; } = default!;
        public DateTime Write_Date { get; set; } = DateTime.UtcNow;
        public string Item_Id { get; set; } = default!;
    }
}
