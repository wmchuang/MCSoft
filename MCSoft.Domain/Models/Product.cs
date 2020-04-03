using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Img { get; set; }

        public decimal Price { get; set; } = 0m;

        public uint Stock { get; set; } = 0;

        public uint SaleCounts { get; set; } = 0;
    }
}
