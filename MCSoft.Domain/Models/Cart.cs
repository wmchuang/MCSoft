using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class Cart : FullAuditedAggregateRoot<Guid>
    {
        public Guid ProductId { get; set; }

        public uint Count { get; set; }

        public Guid UserId { get; set; }
    }
}
