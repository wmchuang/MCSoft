using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Application.Dto.Product
{
    public class ProductSaveDto : ProductUpdateDto
    {
        public new Guid? Id { get; set; }
    }
}
