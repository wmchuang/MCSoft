using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 团长
    /// </summary>
    public class Head : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string WxNumber { get; set; }

        /// <summary>
        /// 小区名
        /// </summary>
        public string CellName { get; set; }

        public string Remark { get; set; }

        public Address Address { get; protected set; }
    }
}
