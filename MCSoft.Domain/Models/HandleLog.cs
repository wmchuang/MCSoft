using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace MCSoft.Domain.Models
{
    public class HandleLog : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public string Level { get; set; }

        public string Message { get; private set; }

        public Guid? TenantId { get; private set; }

        private HandleLog()
        {
        }

        public HandleLog(string level, string message, Guid? tenantId) : this()
        {
            Level = level;
            Message = message;
            TenantId = tenantId;

        }
    }
}
