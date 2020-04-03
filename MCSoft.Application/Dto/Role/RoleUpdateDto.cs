using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto
{
    public class RoleUpdateDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
