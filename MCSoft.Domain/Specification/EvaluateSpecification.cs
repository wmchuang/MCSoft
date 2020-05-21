using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace MCSoft.Domain.Specification
{
    //Dev
    public class EvaluateSpecification : Specification<Evaluate>
    {

        public Guid? HeadId { get; set; }



        public EvaluateSpecification(Guid? headId)
        {
            this.HeadId = headId;

        }

        public override Expression<Func<Evaluate, bool>> ToExpression()
        {
            Expression<Func<Evaluate, bool>> exp = x => true;
            if (this.HeadId.HasValue && this.HeadId.Value != Guid.Empty)
            {
                exp = exp.And(x => x.HeadId == HeadId);

            }

            return exp;
        }
    }
}
