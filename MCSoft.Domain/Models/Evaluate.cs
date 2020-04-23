using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 评价
    /// </summary>
    public partial class Evaluate : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid HeadId { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string Content { get; set; }

        public virtual User User { get; set; }

        public virtual Head Head { get; set; }
    }

    public partial class Evaluate
    {
        private Evaluate() { }


        public Evaluate(Guid userId, Guid headId, string content)
        {
            Id = Guid.NewGuid();
            this.UserId = userId;
            this.HeadId = headId;
            this.Content = content;
        }
    }
}
