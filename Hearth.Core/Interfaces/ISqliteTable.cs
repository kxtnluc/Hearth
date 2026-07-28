using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Interfaces
{
    public interface ISqliteTable
    {
        int Id { get; set; }
        //DateTime Timestamp { get; set; }
        //bool Validate(out string error, out HashSet<string> invalidFields);
    }
}
