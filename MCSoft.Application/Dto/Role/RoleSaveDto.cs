using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto.Role
{
    public class RoleSaveDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public string[] MenuNames { get; set; }
    }
}
