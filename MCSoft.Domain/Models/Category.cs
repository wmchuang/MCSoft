using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    public class Category : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Img { get; set; }
    }
}
