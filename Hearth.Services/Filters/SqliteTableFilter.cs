using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Filters
{
    /// <summary>
    /// This isn't really supposed to be used, but its more of an implementation for the interfaces for ones i havn't gotten to yet, or for
    /// whatever reason might not need a filter, but still wish to inherit from ISqliteTableService
    /// </summary>
    public class SqliteTableFilter
    {
        public int? Id { get; set; } = null;
    }
}
