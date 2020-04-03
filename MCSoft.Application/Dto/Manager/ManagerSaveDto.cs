using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto
{
    public class ManagerSaveDto
    {
        public Guid? Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsDisabled { get; set; }

        public Guid[] RoleIds { get; set; }

    }
}
