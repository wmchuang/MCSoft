using MCSoft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Volo.Abp.Specifications;

namespace MCSoft.Domain.Specification
{
    public class HeadSpecification : Specification<Head>
    {

        public string Keyword { get; set; }



        public HeadSpecification(string keyword)
        {
            this.Keyword = keyword;
          
        }

        public override Expression<Func<Head, bool>> ToExpression()
        {
            Expression<Func<Head, bool>> exp = x => true;
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                exp = exp.And(x => x.Name.Contains(Keyword) || (x.Phone == Keyword) );
            }
            
            return exp;
        }
    }
}
