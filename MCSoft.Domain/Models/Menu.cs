using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public Guid ParentId { get; set; }
    }
}
