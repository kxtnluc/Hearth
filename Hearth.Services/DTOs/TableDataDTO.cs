using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs
{
    // the double underscore "__" prefix indicates that this is a DTO not directly related to any table in the database.
    public class __TableDataDTO
    {
        public bool Exists { get; set; }
        public bool IsEmpty { get; set; }
        public int RowCount { get; set; }
    }
}
