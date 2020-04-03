using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace MCSoft.Application
{
    public class PersonDto : AuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public Guid? TenantId { get; set; }
    }
}
