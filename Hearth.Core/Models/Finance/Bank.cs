using Hearth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    /// <summary>
    /// This class represents a bank user access token plaid item. Nothing more, nothing less. DO NOT Confused with Institution.
    /// </summary>
    public class Bank : ISqliteTable
    {
        /// <summary>
        /// Primary key for the Bank table. Referenced as [BankId] in other tables.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign key for the [User] table. This is the user that owns the access token.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The ITEM ID provided by Plaid.
        /// </summary>
        public string Item_Id { get; set; } = string.Empty;
        /// <summary>
        /// The access token provided by Plaid. This is used to make API calls to Plaid on behalf of the user.
        /// </summary>
        public string Access_Token { get; set; } = string.Empty;
        /// <summary>
        /// Unsure what this does lowkey :D TODO findout
        /// </summary>
        public string? Request_Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Institution_Id { get; set; }
        #region Tracking
        public DateTime? InitalDateRequested { get; set; }
        public DateTime? LastDateRequested { get; set; }
        public DateTime? LastModified { get; set; }
        #endregion
    }
}
