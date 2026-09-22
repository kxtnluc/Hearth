using System;
using System.Collections.Generic;
using System.Reflection;
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

        /// <summary>
        /// Resets every writable property on this filter (including subclass properties)
        /// back to its default value — same as a freshly-constructed instance.
        /// </summary>
        public virtual void Reset()
        {
            var blank = Activator.CreateInstance(GetType());

            foreach (var prop in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.CanWrite)
                {
                    prop.SetValue(this, prop.GetValue(blank));
                }
            }
        }
    }
}
