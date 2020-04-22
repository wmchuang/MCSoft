using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    public class Role : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
