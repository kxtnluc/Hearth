using Hearth.Core.Interfaces;
using Hearth.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    public class TransactionCategoryRule : IRule
    {
        #region Key Props
        public int Id { get; set; }
        #region IRule Specific Fields
        public string Name { get; set; } = default!;
        public bool Active { get; set; } = true;
        #endregion
        public int Priority { get; set; }
        #endregion
        #region Assignment Prop
        /// <summary>
        /// The category that is assigned to the object being evaluated, IF the rule's conditions are met
        /// </summary>
        /// <example>2</example>
        public int TransactionCategoryId { get; set; }
        #endregion
    }
}
