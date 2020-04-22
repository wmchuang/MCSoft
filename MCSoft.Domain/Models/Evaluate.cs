using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 评价
    /// </summary>
    public class Evaluate : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid HeadId { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }

        public virtual User User { get; set; }
    }
}
