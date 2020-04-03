using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace MCSoft.Domain
{
    public class Phone : Entity<Guid>
    {
        public virtual Guid PersonId { get; set; }

        public virtual string Number { get; set; }

        public virtual PhoneType Type { get; set; }

        private Phone()
        {

        }

        public Phone(Guid personId, string number, PhoneType type = PhoneType.Mobile)
        {
            Id = Guid.NewGuid();
            PersonId = personId;
            Number = number;
            Type = type;
        }

        public override object[] GetKeys()
        {
            return new object[] { PersonId, Number };
        }
    }
}
