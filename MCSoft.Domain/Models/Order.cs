using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace MCSoft.Domain.Models
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

        public Guid HeadId { get; set; }

        public string OrderNo { get; set; }

        public uint ProductCount { get; set; }

        public decimal Amount { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public virtual List<OrderItem> OrderItems { get; protected set; }

        public Address Address { get; set; }


    }
}
