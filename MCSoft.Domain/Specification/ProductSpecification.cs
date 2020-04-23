using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace MCSoft.Domain.Specification
{
    public class ProductSpecification : Specification<Product>
    {

        public string Name { get; set; }

        public Guid? HeadId { get; set; }

        public bool? IsEnable { get; set; }

        public ProductSpecification(string name, Guid? headId, bool? isEnable)
        {
            this.Name = name;
            this.HeadId = headId;
            this.IsEnable = isEnable;
        }

        public override Expression<Func<Product, bool>> ToExpression()
        {
            Expression<Func<Product, bool>> exp = x => true;
            if (!string.IsNullOrWhiteSpace(Name))
            {
                exp = exp.And(x => x.Name.Contains(Name));
            }
            if (HeadId.HasValue && HeadId.Value != Guid.Empty)
            {
                exp = exp.And(x => x.HeadId == HeadId);
            }
            if (IsEnable.HasValue)
            {
                exp = exp.And(x => x.IsEnable == IsEnable);
            }

            return exp;
        }
    }
}
