using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application.Dto.Product
{
    public class ProductSearchInput : SearchInput
    {
        public Guid HeadId { get; set; }

        public bool? IsEnable { get; set; }
    }
}
