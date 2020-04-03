using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace MCSoft.Domain.Models
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; set; }

        public virtual Guid ProductId { get; protected set; }

        public virtual int Count { get; protected set; }

        public virtual int Price { get; protected set; }


        public override object[] GetKeys()
        {
            return new Object[] { OrderId, ProductId };
        }
    }
}
