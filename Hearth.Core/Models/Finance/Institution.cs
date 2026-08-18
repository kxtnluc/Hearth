// PLAID API
using Hearth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    /// <summary>
    /// The Actual Bank Companies. TODO: Ignore for now
    /// </summary>
    public class Institution : ISqliteTable
    {
        /// <summary>
        /// This is the primary key for the Bank table. Refered to as [BankId] in other tables.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// This is Plaid's Institution Id that gets returned
        /// </summary>
        public string Institution_Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}