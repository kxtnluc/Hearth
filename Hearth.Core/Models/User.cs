using Hearth.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hearth.Core.Models
{
    public class User : ISqliteTable
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Pin { get; set; } = default!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = new Role();
    }
}
