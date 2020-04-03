using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application.Dto.Role
{
    public class RoleMenuDto
    {
        public RoleDto RoleDto { get; set; }

        public List<string> MenuNames { get; set; }
    }
}
