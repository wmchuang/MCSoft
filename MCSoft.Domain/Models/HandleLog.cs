using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    public class HandleLog : FullAuditedAggregateRoot<Guid>
    {
        public string Level { get; set; }

        public string Message { get; private set; }

        private HandleLog()
        {

        }

        public HandleLog(string level, string message) : this()
        {
            Level = level;
            Message = message;
        }
    }
}
