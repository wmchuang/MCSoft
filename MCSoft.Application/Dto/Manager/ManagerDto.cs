using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application.Dto
{
    public class ManagerDto : FullAuditedEntityDto<Guid>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool IsDisabled { get; set; }

    }
}
