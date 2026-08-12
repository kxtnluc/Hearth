using Hearth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models
{
    public class Role : ISqliteTable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}