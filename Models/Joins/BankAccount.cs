using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models.Joins
{
    public class BankAccount
    {
        public int UserId { get; set; }
        public string AccountId { get; set; }
        public string? Account_Name { get; set; }
        public string BankId { get; set; }
        public string? Bank_Name { get; set; }
        public string? Access_Token { get; set; }
    }
}
