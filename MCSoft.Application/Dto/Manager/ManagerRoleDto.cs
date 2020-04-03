using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application.Dto.Manager
{
    public class ManagerRoleDto
    {
        public ManagerDto ManagerDto { get; set; }

        public List<RoleDto> RoleList { get; set; }
    }
}
