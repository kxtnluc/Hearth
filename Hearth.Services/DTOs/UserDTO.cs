using Hearth.Core.Models;
using Hearth.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs
{
    public class UserDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Pin { get; set; } = default!;
        public int RoleId { get; set; }
        public Role Role { get; set; } = new Role();
    }
}
